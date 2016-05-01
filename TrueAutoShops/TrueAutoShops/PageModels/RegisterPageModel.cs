using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshMvvm;
using TrueAutoShops.Models;
using TrueAutoShops.Models.Response;
using TrueAutoShops.NavigationServices;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    public class RegisterPageModel : BaseViewModel<UserProfile>
    {


        #region private members
        CancellationTokenSource _lastCancelSource;
        private readonly ISecurityDataService _securityDataService;


        #endregion
        public RegisterPageModel(ISecurityDataService securityDataService)
        {
            _securityDataService = securityDataService;

        }

        public override void Init(object initData)
        {

            base.Init(initData);
            Model = new UserProfile();
            CurrentPage.Title = "Create an Account";
            CurrentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "",
                Order = ToolbarItemOrder.Primary
            });
        }

        private async Task RegisterUser(UserProfile userProfile)
        {
            IsBusy = true;
            // Stop previous _search
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var token = _lastCancelSource.Token;
            userProfile.RoleName = "tas-mobile";
            userProfile.ConfirmPassword = userProfile.Password;
            userProfile.UserName = userProfile.Email;

            var createProfileResponse = await _securityDataService.RegisterUser(token, userProfile);

            if (createProfileResponse.Email == string.Empty) return;

            var page = FreshPageModelResolver.ResolvePageModel<DashboardPageModel>();
            var tabbedNavigations = DashboardNavigation.CreateNavigation();

            await CoreMethods.PushNewNavigationServiceModal(tabbedNavigations, new[] { page.GetModel() });
            IsBusy = false;

        }

        public Command RegisterUserCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    await RegisterUser(Model);
                });
            }
        }
    }
}
