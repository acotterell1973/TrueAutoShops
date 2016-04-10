using TrueAutoShops.Models;
using TrueAutoShops.Services;

namespace TrueAutoShops.PageModels
{
    public class ProfileVehiclePageModel : FreshMvvm.FreshBasePageModel
    {
        private readonly ISecurityDataService _securityDataService;


        public UserProfile UserProfile { get; set; }

        public ProfileVehiclePageModel(ISecurityDataService securityDataService)
        {
            _securityDataService = securityDataService;

        }

        public override void Init(object initData)
        {

            base.Init(initData);
            UserProfile = new UserProfile();
        }


    }
}
