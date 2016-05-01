using System;
using Xamarin.Forms;

namespace TrueAutoShops.Extensions
{
    public interface IImageUrlTransformer
    {
        string TransformForCurrentPlatform(string url);
    }

    public class ImageUrlTransformer : IImageUrlTransformer
    {
        public string TransformForCurrentPlatform(string url)
        {
            var result = ArgumentValidator.AssertNotNull(url, "url");

            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
            {
                const string filePrefix = "file:///";

                if (url.StartsWith(filePrefix))
                {
                    result = url.Substring(filePrefix.Length);
                }

                result = result.Replace("/", "_").Replace("\\", "_");

                if (result.StartsWith("_") && result.Length > 1)
                {
                    result = result.Substring(1);
                }
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                if (url.StartsWith("/") && url.Length > 1)
                {
                    result = result.Substring(1);
                }
            }

            return result;
        }

    }

    public static class ArgumentValidator
    {
        public static string AssertNotNull(string value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }
    }
}
