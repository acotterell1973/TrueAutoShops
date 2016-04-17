using FreshMvvm;
using PropertyChanged;
using TrueAutoShops.Models;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    [ImplementPropertyChanged]
    public class LoginPageModel : BaseViewModel<Login>
    {
        private readonly ISecurityDataService _securityDataService;
        //   private readonly IUserDialogs _userDialogs;

        public LoginPageModel(ISecurityDataService securityDataService)
        {
            _securityDataService = securityDataService;
            //   _userDialogs = userDialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new Login() { Email = "acotterell1973@gmail.com", Password = "MercedesCLK430!" };
        }

        #region Commands

        public Command SiginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var token = await _securityDataService.LoginUser(Model);
                    if (token.AccessToken == string.Empty) return;
                    
                    var page = FreshPageModelResolver.ResolvePageModel<DashboardPageModel>();
                    
                    var tabbedNavigations = new FreshTabbedNavigationContainer();
                    tabbedNavigations.AddTab<SearchShopsPageModel>("Search", "Search-30.png");
                    tabbedNavigations.AddTab<ServiceShopHistoryPageModel>("Previous Shops", "");
                    tabbedNavigations.AddTab<ProfilePageModel>("Profile", "Gender-Neutral-User-30");

                    await CoreMethods.PushNewNavigationServiceModal(tabbedNavigations, new[] { page.GetModel() });
                    IsBusy = false;
                });
            }
        }


        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RegisterPageModel>();
                });
            }
        }
        #endregion
    }
}
