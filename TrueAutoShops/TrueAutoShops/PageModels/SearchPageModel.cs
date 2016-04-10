using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreshMvvm;
using PropertyChanged;
using TrueAutoShops.Models;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    [ImplementPropertyChanged]
    public class SearchPageModel : BaseViewModel<ShopInfo>
    {
        #region private members
        CancellationTokenSource _lastCancelSource;
        private readonly ISecurityDataService _securityDataService;
        private readonly IShopDataService _shopDataService;
        Search search;
        #endregion

        #region Properties
        public string SearchText { get; set; } = string.Empty;
        public List<ShopInfo> Shops { get; set; }
        public SearchProperty SearchProperty { get; set; }
        public Search SearchFilter { get; set; }
        #endregion


        public SearchPageModel(ISecurityDataService securityDataService, IShopDataService shopDataService)
        {
            _securityDataService = securityDataService;
            _shopDataService = shopDataService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new ShopInfo();
        }


        public async void Search()
        {
            // Stop previous search
            _lastCancelSource?.Cancel();

            // Perform the search
            _lastCancelSource = new CancellationTokenSource();
            var token = _lastCancelSource.Token;
            var results = await _shopDataService.GetShopsByCityId(token, 99);

        }

        #region Commands

        public Command SearchCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    // Stop previous search
                    _lastCancelSource?.Cancel();

                    // Perform the search
                    _lastCancelSource = new CancellationTokenSource();
                    var token = _lastCancelSource.Token;
                    int zipCode;
                    int.TryParse(SearchText, out zipCode);
                    var results = await _shopDataService.GetShopsByCityId(token, zipCode);
                });
            }
        }

        #endregion
    }
}
