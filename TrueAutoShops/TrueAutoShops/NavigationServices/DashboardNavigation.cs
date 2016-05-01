using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using TrueAutoShops.PageModels;

namespace TrueAutoShops.NavigationServices
{
    public class DashboardNavigation
    {
        public static FreshTabbedNavigationContainer CreateNavigation()
        {

            var tabbedNavigations = new FreshTabbedNavigationContainer();
            tabbedNavigations.AddTab<SearchShopsPageModel>("Search", "Search-30.png");
            tabbedNavigations.AddTab<ServiceShopHistoryPageModel>("Previous Shops", "");
            tabbedNavigations.AddTab<ProfilePageModel>("Profile", "Gender-Neutral-User-30");

            return tabbedNavigations;
        }
    }
}
