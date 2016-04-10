using System;
using System.Threading.Tasks;
using FreshMvvm;
using TrueAutoShops.PageModels;
using Xamarin.Forms;

namespace TrueAutoShops.NavigationServices
{
    public class DashboardTabbedNavigationContainer : MasterDetailPage, IFreshNavigationService
    {
        private FreshTabbedNavigationContainer _tabbedNavigations;
        private Page _searchPage;
        private Page _shopServiceHistoryPage;


        public DashboardTabbedNavigationContainer()
        {
            NavigationServiceName = Helpers.Constants.AppDefaultNavigationServiceName;
            SetupTabbedPage();
            CreateMenuPage("TAS");
            RegisterNavigation();

        }
        public virtual async Task PopToRoot(bool animate = true)
        {
            await ((NavigationPage)_tabbedNavigations.CurrentPage).PopAsync(animate);
        }

        public virtual async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
            {
                await Navigation.PushModalAsync(new NavigationPage(page), animate);
            }
            else
            {
                await ((NavigationPage)_tabbedNavigations.CurrentPage).PushAsync(page, animate);
            }
        }

        public virtual async Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await ((NavigationPage)_tabbedNavigations.CurrentPage).PopAsync();
            }
        }

        public void NotifyChildrenPageWasPopped()
        {
            throw new NotImplementedException();
        }

        public string NavigationServiceName { get; }

        protected virtual void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this);
        }

        protected void CreateMenuPage(string menuPageTitle)
        {
            var menuPage = new ContentPage { Title = menuPageTitle };
            var listView = new ListView { ItemsSource = new string[] { "Search", "Previous Services" } };

            listView.ItemSelected += (sender, args) =>
            {
                switch ((string)args.SelectedItem)
                {
                    case "search":
                        _tabbedNavigations.CurrentPage = _searchPage;
                        break;
                    case "service_history":
                        _tabbedNavigations.CurrentPage = _shopServiceHistoryPage;
                        break;
                }

                IsPresented = false;
            };
            menuPage.Content = listView;
            Master = new NavigationPage(menuPage)
            {
                Title = "TASs",
             //   Icon = ImageSource.FromFile("icon.png") as FileImageSource,
                BackgroundColor = Color.FromHex("333333")
            };
            

        }

        private void SetupTabbedPage()
        {
            try
            {
                _tabbedNavigations = new FreshTabbedNavigationContainer();
                _searchPage = _tabbedNavigations.AddTab<SearchPageModel>("Search", "");
                _shopServiceHistoryPage = _tabbedNavigations.AddTab<ServiceShopHistoryPageModel>("Previous Shops", "");
                this.Detail = _tabbedNavigations;
            }
            catch (Exception exception)
            {
                
                throw;
            }
        
        }

    }
}
