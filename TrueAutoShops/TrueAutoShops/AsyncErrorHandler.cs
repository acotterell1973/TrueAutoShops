using System;
using System.Diagnostics;
using Xamarin;

namespace TrueAutoShops
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Insights.Report(exception);
        }
    }
}
