using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using MessMSPrism.Models;
using MessMSPrism.Resources.HelperClasses;

namespace MessMSPrism.ViewModels
{
    public class UpdateStudentFormViewModel : BindableBase
    {

        #region Fields

        private IEnumerable<Student> _students;

        #endregion
        #region Properties

        public IEnumerable<Student> Students
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }

        public Repository<Student> Repository { get; set; }



        #endregion
        public UpdateStudentFormViewModel()
        {
            Repository = new Repository<Student>();
            Students = Repository.GetAll();
        }
    }
}
