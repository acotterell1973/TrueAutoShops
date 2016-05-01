using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrueAutoShops.Models;

namespace TrueAutoShops.Helpers
{
    internal static class HttpClientConnector 
    {
        private static readonly Lazy<HttpClient> HttpClientConnection = new Lazy<HttpClient>(()=>
        {
            
            var baseAddress = new Uri("https://services.techverseenterprise.com:444");
            var cookieContainer = new CookieContainer();
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };

            if (httpClientHandler.SupportsAutomaticDecompression)
            {
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var authData = $"{""}:{""}";
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));


            var client = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress,
                MaxResponseContentBufferSize = 256000
            };

            
            client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeaderValue);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MediaTypeNames.ApplicationJson));
            return client;


        }, LazyThreadSafetyMode.ExecutionAndPublication);

        
        public static HttpClient Instance => HttpClientConnection.Value;

        internal static async Task<TokenResponseModel> GetBearerToken(string username = "tasserviceaccount", string password = "MercedesCLK430!", string siteUrl = null)
        {
            var client = new HttpClient {BaseAddress = new Uri(siteUrl ?? "https://services.techverseenterprise.com:444") };
            client.DefaultRequestHeaders.Accept.Clear();

            var tokenRequest = new
            {
                username = "tasserviceaccount",
                password = "MercedesCLK430!",
                grant_type = "password",
                client_id = "ab1ba6d1e3904d9e8bab4e3ba6fb8f9b"
            };

            var postValues = new Dictionary<string, string>
                            {
                                {"username",tokenRequest.username},{"password",tokenRequest.password},{"grant_type",tokenRequest.grant_type},{"client_id",tokenRequest.client_id}
                            };


            var content = new FormUrlEncodedContent(postValues);


            var response = await client.PostAsync("/oauth2/token", content);
            if (!response.IsSuccessStatusCode) { return null; }

            string jsonMessage;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                jsonMessage = new StreamReader(responseStream).ReadToEnd();
            }

            var tokenResponse = (TokenResponseModel)JsonConvert.DeserializeObject(jsonMessage, typeof(TokenResponseModel));

            return tokenResponse;


        }

    }
}
