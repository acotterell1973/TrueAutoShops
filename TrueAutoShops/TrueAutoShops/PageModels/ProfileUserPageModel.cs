using TrueAutoShops.Models;
using TrueAutoShops.Services;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    public class ProfileUserPageModel : FreshMvvm.FreshBasePageModel
    {
        private readonly ISecurityDataService _securityDataService;


        public UserProfile UserProfile { get; set; }

        public ProfileUserPageModel(ISecurityDataService securityDataService)
        {
            _securityDataService = securityDataService;

        }

        public override void Init(object initData)
        {

            base.Init(initData);
            UserProfile = new UserProfile();
            CurrentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Save",
                
                Order = ToolbarItemOrder.Primary
              
            });
        }

    
    }
}
