using FreshMvvm;
using TrueAutoShops.Helpers;
using TrueAutoShops.NavigationServices;
using TrueAutoShops.PageModels;
using TrueAutoShops.Services;
using TrueAutoShops.Styles;
using Xamarin.Forms;
using Constants = TrueAutoShops.Helpers.Constants;

namespace TrueAutoShops
{
    public class App : Application
    {

        public App()
        {
            RegisterDependancies();
            RegisterRootNavigation();
        }

        private void RegisterMasterDetail()
        {
            var masterDetailNavigationContainer = new ThemedMasterDetailNavigationContainer();
            masterDetailNavigationContainer.Init("Menu","slideout.png");
           // masterDetailNavigationContainer.AddPageWithIcon<SearchPageModel>("Home","slideout.png");
            FreshIOC.Container.Register<IFreshNavigationService>(masterDetailNavigationContainer);
        }

        private void RegisterRootNavigation()
        {

            Resources = new AppStyleResources().Dictionary;
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

            var logginNavigation = new FreshNavigationContainer(page, Constants.LoginNavigationService);
            
            var dashboardNavigation = new DashboardTabbedNavigationContainer();
            
            MainPage = logginNavigation;
        }
        private static void RegisterDependancies()
        {
            Akavache.BlobCache.ApplicationName = Constants.CacheName;
            FreshIOC.Container.Register<IShopDataService, ShopDataService>();
            FreshIOC.Container.Register<ISecurityDataService, SecurityDataService>();
            FreshIOC.Container.Register(HttpClientConnector.Instance);
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
