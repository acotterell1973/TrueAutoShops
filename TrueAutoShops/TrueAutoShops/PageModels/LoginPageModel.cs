using System.Threading;
using FreshMvvm;
using PropertyChanged;
using TrueAutoShops.Models;
using TrueAutoShops.NavigationServices;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    [ImplementPropertyChanged]
    public class LoginPageModel : BaseViewModel<Login>
    {
        CancellationTokenSource _lastCancelSource;
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
                    _lastCancelSource?.Cancel();

                    // Perform the _search
                    _lastCancelSource = new CancellationTokenSource();
                    var cancellationToken = _lastCancelSource.Token;
                    var token = await _securityDataService.LoginUser(cancellationToken, Model);
                    if (token.AccessToken == string.Empty) return;

                    var page = FreshPageModelResolver.ResolvePageModel<DashboardPageModel>();
                    var tabbedNavigations = DashboardNavigation.CreateNavigation();

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
