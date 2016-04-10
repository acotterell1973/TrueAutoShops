using PropertyChanged;
using TrueAutoShops.Models;
using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
    [ImplementPropertyChanged]
    public class ProfilePageModel : BaseViewModel<Profile>
    {

        public Command MapNavCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    if (param == null) return;
                    switch ((string) param)
                    {
                        case "user":
                            await CoreMethods.PushPageModel<ProfileUserPageModel>();
                            break;
                        case "vehicle":
                            await CoreMethods.PushPageModel<ProfileVehiclePageModel>();
                            break;
                    }
                });
            }
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new Profile();
        }

    }
}
