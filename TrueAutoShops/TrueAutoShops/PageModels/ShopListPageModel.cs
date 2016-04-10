using System.Collections.Generic;
using TrueAutoShops.Models;
using TrueAutoShops.Services;

namespace TrueAutoShops.PageModels
{
    public class ShopListPageModel : FreshMvvm.FreshBasePageModel
    {

        private readonly IShopDataService _iShopDataService;
     //   private readonly IUserDialogs _userDialogs;

        public List<ShopInfo> Shops { get; set; }
        public ShopListPageModel(IShopDataService shopDataService)
        {
            _iShopDataService = shopDataService;
          //  _userDialogs = userDialogs;
        }

        #region Event Commands
        #endregion
        public override async void Init(object initData)
        {
            base.Init(initData);
         //   _userDialogs.ShowLoading();
            Shops = await _iShopDataService.GetListofShopsByCityName("");
         //   _userDialogs.HideLoading();
        }
    }
}
