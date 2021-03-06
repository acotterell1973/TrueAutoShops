﻿using System.Collections.Generic;
using System.Threading;
using PropertyChanged;
using TrueAutoShops.Models;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    public class SearchPageModelx : BaseViewModel<List<ShopInfo>>
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


        public SearchPageModelx(ISecurityDataService securityDataService, IShopDataService shopDataService)
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


        #endregion
    }
}
