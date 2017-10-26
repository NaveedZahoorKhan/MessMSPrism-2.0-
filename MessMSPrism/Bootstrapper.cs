using Microsoft.Practices.Unity;
using Prism.Unity;
using MessMSPrism.Views;
using System.Windows;
using Prism.Regions;

namespace MessMSPrism
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        private RegionManager _regionManager;
        protected override void InitializeShell()
        {
          
            Application.Current.MainWindow.Show();
            _regionManager = new RegionManager();
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(Dashboard));
            
            Container.RegisterTypeForNavigation<StudentForm>();
            Container.RegisterTypeForNavigation<UpdateStudentForm>();
            Container.RegisterTypeForNavigation<Billing>();
            Container.RegisterTypeForNavigation<Dishes>();
            Container.RegisterTypeForNavigation<Attendance>();



        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
        }
    }
}
