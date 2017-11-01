using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;

namespace MessMSPrism.Resources.HelperClasses
{
    class Navigation
    {
        private IRegionManager _regionManager;
        public Navigation(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Navigate(string path)
        {
            if (path != null)
            {
                _regionManager.RequestNavigate("MainRegion", path);
            }
        }
    }
}
