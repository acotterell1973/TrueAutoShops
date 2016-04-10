using Xamarin.Forms;

namespace TrueAutoShops.PageModels
{
   public class DashboardPageModel: FreshMvvm.FreshBasePageModel
    {
        public override void Init(object initData)
        {

            base.Init(initData);
         
           // var page = this.CurrentPage.Navigation.NavigationStack.First();
        //   var x = this.CurrentPage.Parent;
        }

        #region Commands
        public Command ScheduleServiceCommand
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
