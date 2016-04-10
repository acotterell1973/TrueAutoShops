using Xamarin.Forms;

namespace TrueAutoShops.Styles
{
    public class AppStyleResources
    {
        public AppStyleResources()
        {
            InitStaticResource();
            InitDynamicResource();
        }
        static readonly ResourceDictionary ResourceDictionary = new ResourceDictionary();

        private static void InitStaticResource()
        {
            var content = new Style(typeof (ContentPage))
            {
              
            };

            var frameStyle = new Style(typeof (Frame))
            {
                Setters =
                {
                    new Setter {Property = Layout.PaddingProperty, Value = new Thickness(0, 0, 0, 0)},
                    new Setter {Property = Frame.OutlineColorProperty, Value = Color.Transparent},
                    new Setter { Property = Frame.HasShadowProperty, Value = false}
                }
            };


            var entryStyle = new Style(typeof(Entry))
            {  
                Setters = {
                    new Setter { Property = Entry.PlaceholderColorProperty, Value = Color.Silver },
                  
                }
            };

            ResourceDictionary.Add(frameStyle);
            ResourceDictionary.Add(entryStyle);
            ResourceDictionary.Add(StaticStyleConstants.TextBoxStyle, entryStyle);
        }

        private static void InitDynamicResource()
        {

        }


        public ResourceDictionary Dictionary => ResourceDictionary;
    }
}