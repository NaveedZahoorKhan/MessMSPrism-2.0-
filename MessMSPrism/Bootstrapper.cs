using Microsoft.Practices.Unity;
using Prism.Unity;
using MessMSPrism.Views;
using System.Windows;

namespace MessMSPrism
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
