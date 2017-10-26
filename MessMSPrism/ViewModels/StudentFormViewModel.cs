using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using QRCoder;

namespace MessMSPrism.ViewModels
{
    public class StudentFormViewModel : BindableBase
    {
        #region private fields

        private static string _sampleMale = "";
        private string _cnic = String.Empty;
        private string _room = null;
        private string _cell = null;
        private string _mess = String.Empty;
        private string _name = String.Empty;
        private string _roll = String.Empty;
        private string _qrCode = string.Empty;
        private FirstTimeSetup _firstTimeSetup;
        private SqlCeConnection _sqlCeConnection;
        private SqlCeCommand _sqlCeCommand;
        private static string _imgAddress;
        private string _img;
        private string selectMaxID => @"select max(id) from student;";

        #endregion
        #region Properties

        public string Cnic
        {
            get { return _cnic; }
            set { _cnic = value; }
        }
        public string Name

        {
            get { return _name; }
            set
            {
                _name = value;
                //INotifyPropertyChanged("Name");
            }
        }
        public string Roll
        {
            get { return _roll; }
            set { _roll = value; }
        }
        public string Mess

        {
            get { return _mess; }
            set
            {
                _mess = value;
                //NotifyPropertyChanged("Mess");
                GenerateQr(_mess);
            }
        }
        public string QrCode
        {
            get { return _qrCode; }
            set { _qrCode = value; }
        }

        public string Room
        {
            get { return _room; }
            set
            {
                _room = value;
                //NotifyPropertyChanged("Room");
            }
        }

        public  string DefaultMale
        {
            get { return _sampleMale; }
            set
            {
                SetProperty(ref value, _sampleMale);
                OnPropertyChanged("DefaultMale");
            }
        }

        public string Cell
        {
            set
            {
                _cell = value;
                  
            }
            get { return _cell; }
        }

        public static string ImgAddress
        {
            set
            {
                _imgAddress = value;
                _sampleMale = null;
                _sampleMale = value;

                //  NotifyPropertyChanged("imgAddress");   
            }
            get { return _imgAddress; }
        }

        private string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        #endregion

        #region DelegateCommands

        public DelegateCommand BrowseButtonClick { get; private set; }
        public  DelegateCommand Save { get; private set; }
        public DelegateCommand Cancel { get; private set; }


        #endregion

        #region Methods

        private void CancelMethod()
        {

        }

        private void SaveStudent()
        {

            if (_sqlCeConnection.State != ConnectionState.Open)
            {
                _sqlCeConnection.Open();
            }
            string command = "insert into student(name, roll, room, qr, photo, cnic) values (@name, @roll, @room, @qr, @photo, @cnic)";
            _sqlCeCommand = new SqlCeCommand(command, _sqlCeConnection);
            _sqlCeCommand.Parameters.AddWithValue("@name", Name);
            _sqlCeCommand.Parameters.AddWithValue("@roll", Roll);
            _sqlCeCommand.Parameters.AddWithValue("@room", Room);
            _sqlCeCommand.Parameters.AddWithValue("@qr", QrCode);
            _sqlCeCommand.Parameters.AddWithValue("@photo", DefaultMale);
            _sqlCeCommand.Parameters.AddWithValue("@cnic", Cnic);


            int res = _sqlCeCommand.ExecuteNonQuery();
            if (res < 0)
            {
                Console.WriteLine("Error while updating table");
            }
        }

        static StudentFormViewModel _studentFormViewModel = new StudentFormViewModel();
        private static void BrowseFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG (.jpeg) |*.jpg";
            var res = dialog.ShowDialog();
            if (res == true)
            {
                string fname = dialog.FileName;
                
            }

        }
        public StudentFormViewModel()
        {
            _sqlCeConnection = new SqlCeConnection();
            Save = new DelegateCommand(SaveStudent);
            Cancel = new DelegateCommand(CancelMethod);
            BrowseButtonClick = new DelegateCommand(BrowseFile);
            _sqlCeConnection.ConnectionString = "DataSource = \"MessMs.sdf\"; Password=\"X_Strik3r\"; Persist Security Info = False; ";

            if (!File.Exists("MessMs.sdf"))
                _firstTimeSetup = new FirstTimeSetup();
            else
            {
                FillMess();
            }

        }


        public void FillMess()
        {
            _sqlCeConnection.Open();
            _sqlCeCommand = new SqlCeCommand(selectMaxID, _sqlCeConnection);
            var res = _sqlCeCommand.ExecuteReader();
            //     Console.WriteLine(res.Depth);
            while (res.Read())
            {
                try
                {
                    Mess = res.GetInt32(0).ToString();
                }
                catch (SqlNullValueException e)
                {
                    Mess = 1.ToString();
                }



            }
        }

        public void GenerateQr(string val)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(val, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            string dir = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.Ticks + ".jpg";
            qrCodeImage.Save(dir);
            QrCode = dir;

        }

        #endregion





        #region Classes

        private class FirstTimeSetup
        {
            private string addStudentTable => @"create table student" +
                                              " ( " +
                                              "id int primary key  identity," +
                                              "name nvarchar(30) not null," +
                                              "roll int not null," +
                                              "room int not null," +
                                              "qr nvarchar(255) not null unique," +
                                              "photo nvarchar(255) not null unique," +
                                              "cnic int not null unique ); ";

            public FirstTimeSetup()
            {
                SqlCeConnection sql = new SqlCeConnection
                {
                    ConnectionString =
                        "DataSource = \"MessMs.sdf\"; Password=\"X_Strik3r\"; Persist Security Info = False; "
                };
                SqlCeEngine ceEngine = new SqlCeEngine(sql.ConnectionString);

                ceEngine.CreateDatabase();
                sql.Open();
                SqlCeCommand comm = new SqlCeCommand(addStudentTable, sql);
                var res = comm.ExecuteNonQuery();

            }
        }


        #endregion

    }
}
