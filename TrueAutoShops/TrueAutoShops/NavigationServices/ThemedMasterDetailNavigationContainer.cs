using System.Collections.Generic;
using FreshMvvm;
using Xamarin.Forms;

namespace TrueAutoShops.NavigationServices
{
    public class ThemedMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        public ThemedMasterDetailNavigationContainer()
        {
            //(Helpers.Constants.AppDefaultNavigationServiceName);
        }
        internal ThemedMasterDetailNavigationContainer(string navigationServiceName) : base(navigationServiceName)
        {

        }
        readonly List<MenuItem> _pageIcons = new List<MenuItem>();

        public void AddPageWithIcon<T>(string title, string icon = "", object data = null) where T : FreshBasePageModel
        {
            AddPage<T>(title, data);
            _pageIcons.Add(new MenuItem()
            {
                Title = title,
                IconSource = icon
            });
        }

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon)
        {
            var listView = new ListView();
            var menuPage = new ContentPage
            {
                Title = menuPageTitle,
                BackgroundColor = Color.FromHex("#c8c8c8")
            };

            listView.ItemsSource = _pageIcons;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetValue(TextCell.TextColorProperty, Color.Black);
            cell.SetBinding(ImageCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            listView.ItemTemplate = cell;
            listView.ItemSelected += (sender, args) =>
            {
                if (Pages.ContainsKey(((MenuItem)args.SelectedItem).Title))
                {
                    Detail = Pages[((MenuItem)args.SelectedItem).Title];
                }
                IsPresented = false;
            };

            menuPage.Content = listView;


            var navPage = new NavigationPage(menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;

            Master = navPage;
        }


        protected override Page CreateContainerPage(Page page)
        {
            var navigation = new NavigationPage(page);
            navigation.BarTextColor = Color.White;

            return navigation;
        }
    }
}


