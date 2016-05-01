using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrueAutoShops.Models;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    public class SearchShopsPageModel : BaseViewModel<List<ShopInfo>>
    {
        #region private members
        CancellationTokenSource _lastCancelSource;
        private readonly ISecurityDataService _securityDataService;
        private readonly IShopDataService _shopDataService;
        private Command<string> _searchCommand;

        #endregion

        #region Properties
        public string SearchText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                CitySearch("Boca");
            }
        } 
        public List<ShopInfo> Shops { get; set; }
        public SearchProperty SearchProperty { get; set; }
        public Search SearchFilter { get; set; }

        public ObservableCollection<ShopsSearch> ShopsSearch { get; set; }
        public ShopsSearch SelectedShopsSearchItem { get; set; }

        #endregion


        public SearchShopsPageModel(ISecurityDataService securityDataService, IShopDataService shopDataService)
        {
            _securityDataService = securityDataService;
            _shopDataService = shopDataService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            ShopsSearch = new ObservableCollection<ShopsSearch>() {new ShopsSearch() {CityName = "Boca Raton"}};
            Model = new List<ShopInfo>();
        }


        private async Task Search(int cityId)
        {
            IsBusy = true;
            // Stop previous _search
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var token = _lastCancelSource.Token;

            Model = await _shopDataService.GetShopsByCityId(token, cityId);
            IsBusy = false;
        }

        private async void CitySearch(string cityName)
        {
            IsBusy = true;
            ShopsSearch.Clear();
            // Stop previous _search
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var token = _lastCancelSource.Token;

            var results = await _shopDataService.GetShopsByCityName(token, cityName);
            foreach (var shopsSearch in results)
            {
                ShopsSearch.Add(shopsSearch);
        
            }
            await Search(ShopsSearch.First().CityId);
            IsBusy = false;
        }
        #region Commands

        public Command SearchCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    int zipCode;
                    int.TryParse(SearchText, out zipCode);
                    await Search(zipCode);
                });
            }
        }

        public Command<string> SearchCitiesCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(
                    obj => { },
                    obj => !string.IsNullOrEmpty(obj.ToString())));
            }
        }

        public Command<ShopsSearch> ShopSelectedCommand
        {
            get
            {
                return new Command<ShopsSearch>(async (param) =>
                {
                    await Search(param.CityId);
                });
            }
        }
        #endregion
    }
}
