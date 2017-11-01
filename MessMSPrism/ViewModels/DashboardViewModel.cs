using Prism.Commands;
using Prism.Mvvm;
using System;
using MessMSPrism.Resources.HelperClasses;
using Prism.Regions;

namespace MessMSPrism.ViewModels
{
    public class DashboardViewModel : BindableBase
    {


        #region private fields

        private readonly IRegionManager _regionManager;
        private Navigation _navigation;

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
            _navigation = new Navigation(regionManager);
            AddStudent = new DelegateCommand<string>(_navigation.Navigate);
            ManageStudent = new DelegateCommand<string>(_navigation.Navigate);
            Billing = new DelegateCommand<string>(_navigation.Navigate);
            Dishes = new DelegateCommand<string>(_navigation.Navigate);
            Attendance = new DelegateCommand<string>(_navigation.Navigate);
        }


        
        #endregion
    }
}
