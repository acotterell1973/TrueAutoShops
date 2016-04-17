using System.Collections.Generic;
using System.Threading;
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

        #endregion

        #region Properties
        public string SearchText { get; set; } = string.Empty;
        public List<ShopInfo> Shops { get; set; }
        public SearchProperty SearchProperty { get; set; }
        public Search SearchFilter { get; set; }
        #endregion


        public SearchShopsPageModel(ISecurityDataService securityDataService, IShopDataService shopDataService)
        {
            _securityDataService = securityDataService;
            _shopDataService = shopDataService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new List<ShopInfo>();
        }


        public async void Search()
        {
            // Stop previous _search
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var token = _lastCancelSource.Token;
            Model = await _shopDataService.GetShopsByCityId(token, 99);

        }

        #region Commands

        public Command SearchCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    IsBusy = true;
                    // Stop previous _search
                    _lastCancelSource?.Cancel();

                    // Perform the _search
                    _lastCancelSource = new CancellationTokenSource();
                    var token = _lastCancelSource.Token;
                    int zipCode;
                    int.TryParse(SearchText, out zipCode);
                    Model = await _shopDataService.GetShopsByCityId(token, zipCode);
                    IsBusy = false;
                });
            }
        }

        #endregion
    }
}
