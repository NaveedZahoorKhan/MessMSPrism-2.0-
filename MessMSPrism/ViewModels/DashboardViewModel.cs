using Prism.Commands;
using Prism.Mvvm;
using System;
using Prism.Regions;

namespace MessMSPrism.ViewModels
{
    public class DashboardViewModel : BindableBase
    {


        #region private fields

        private readonly IRegionManager _regionManager;


        #endregion
        #region Properties



        #endregion
        #region ICommands

        public DelegateCommand<string> AddStudent { get; private set; }

        public DelegateCommand<String> ManageStudent { get; private set; }        
        public DelegateCommand<string> Billing { get; private set; }
        public DelegateCommand<string> Dishes { get; private set; }
        public DelegateCommand<string> Attendance { get; private set; }
        #endregion

        #region Methods

        public DashboardViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            AddStudent = new DelegateCommand<string>(Navigate);
            ManageStudent = new DelegateCommand<string>(Navigate);
            Billing = new DelegateCommand<string>(Navigate);
            Dishes = new DelegateCommand<string>(Navigate);
            Attendance = new DelegateCommand<string>(Navigate);
        }


        private void Navigate(string path)
        {   
            if (path != null)
            {
                _regionManager.RequestNavigate("MainRegion", path);
            }
        }

        #endregion
    }
}
