using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TrueAutoShops.Helpers
{
    internal class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler nextHandler)
        {
            InnerHandler = nextHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
        
            return response;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
             //   _writer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}