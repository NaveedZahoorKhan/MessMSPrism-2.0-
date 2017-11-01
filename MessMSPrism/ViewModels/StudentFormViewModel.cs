using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using MessMSPrism.Models;
using MessMSPrism.Resources.HelperClasses;
using Microsoft.Win32;
using Prism.Regions;
using QRCoder;

namespace MessMSPrism.ViewModels
{
    public class StudentFormViewModel : BindableBase
    {
        #region private fields

        private static string _sampleMale = "../Resources/icons/profile-default-male.png";
        private string _cnic;
        private int _room ;
        private string _cell;
        private string _mess;
        private string _name ;
        private string _roll;
        private string _qrCode;
        private string _finalImage;
        private static string _imgAddress;
        private string _img;
        private readonly IRegionManager _regionManager;
        private Navigation _navigation;
        #endregion
        #region Properties
        public Repository<Student> Repo { get; set; }
        public string Cnic
        {
            get { return _cnic; }
            set
            {
                SetProperty(ref _cnic, value); 
                RaisePropertyChanged("Cnic");
            }
        }

        public string Name

        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                
                RaisePropertyChanged("Name");
            }
        }

        public string Roll
        {
            get { return _roll; }
            set
            {
                SetProperty(ref _roll, value); 
                RaisePropertyChanged("Roll");
            }
        }
        public string Mess

        {
            get { return _mess; }
            set
            {
                SetProperty(ref _mess, value); 
                RaisePropertyChanged("Mess");
            }
        }
        public string QrCode
        {
            get
            {
                return _qrCode; 
                
            }
            set
            {
                SetProperty(ref  _qrCode, value);
                RaisePropertyChanged("QrCode");
            }
        }

        public int Room
        {
            get { return _room; }
            set
            {
                SetProperty(ref _room, value);
                RaisePropertyChanged("Room");
            }
        }
        
        public  string DefaultMale
        {
            get { return _sampleMale; }
            set
            {

                _sampleMale = null;
              SetProperty(ref _sampleMale, value);
                RaisePropertyChanged("DefaultMale");
            }
        }

        public string Cell
        {
            set { SetProperty(ref _cell, value); }
            get { return _cell; }
        }

        public string FinalImage
        {
            set { SetProperty(ref _finalImage, value); }
            get { return _finalImage; }
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
            set { SetProperty(ref _img, value); }
        }

        #endregion

        #region DelegateCommands

        public DelegateCommand BrowseButtonClick { get; private set; }
        public  DelegateCommand Save { get; private set; }
        public DelegateCommand<String> Cancel { get; private set; }


        #endregion

        #region Methods

        private void CheckForEnvironment()
        {
              
        }
        

        private void SaveStudent()
        {
            CheckForEnvironment();
            Student student = new Student()
            {
                RoomNo = Room,
                Cnic = Cnic,
                QrPath =  QrCode,
                Name =  Name,
                ImgPath = FinalImage,
               
            };
            Repo.Insert(student);
         
        }

      
        private void BrowseFile()
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Title = "Please select an Image";
            dialog.Filter = "Image Files | *.jpg;*.png;*.jpeg;";
            var res = dialog.ShowDialog();
            if (!res.Equals(true)) return;
            DefaultMale = dialog.FileName;
           FinalImage = DateTime.Now.Ticks + RandomString(10);

        }
        private static readonly Random Random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        public StudentFormViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _navigation = new Navigation(regionManager);
            Save = new DelegateCommand(SaveStudent);
            Cancel = new DelegateCommand<String>(_navigation.Navigate);
            BrowseButtonClick = new DelegateCommand(BrowseFile);
            Repo = new Repository<Student>();
            FillMess();
        }


        private void FillMess()
        {
            var res = Repo.GetCount();
            Mess = (res + 1).ToString();

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




        

    }
}
