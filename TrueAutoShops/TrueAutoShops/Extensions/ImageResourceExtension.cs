using System;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrueAutoShops.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;

            var transformer = FreshIOC.Container.Resolve<IImageUrlTransformer>();
            ImageSource imageSource = null;

            var url = transformer.TransformForCurrentPlatform(Source);

            switch (Device.OS)
            {
                case TargetPlatform.Android:
                    imageSource = ImageSource.FromFile(url);
                    break;
                case TargetPlatform.iOS:
                    imageSource = ImageSource.FromFile(url);
                    break;
                case TargetPlatform.WinPhone:
#if WINDOWS_PHONE
        if (url.StartsWith("/") && url.Length > 1)
        {
            url = url.Substring(1);
        }

        var stream = System.Windows.Application.GetResourceStream(new Uri(url, UriKind.Relative));

        if (stream != null)
        {
            imageSource = ImageSource.FromStream(() => stream.Stream);
        }
        else
        {
            ILog log;
            if (Dependency.TryResolve<ILog>(out log))
            {
               log.Debug("Unable to located create ImageSource using URL: " + url);
            }
        }
#endif
                    break;
            }

            return imageSource ?? (ImageSource.FromFile(url));
        }
    }
}