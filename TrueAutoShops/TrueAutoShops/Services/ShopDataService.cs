using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using Polly;
using TrueAutoShops.Helpers;
using TrueAutoShops.Models;
using TrueAutoShops.Models.Response;
using Xamarin;

namespace TrueAutoShops.Services
{
    public class ShopDataService : IShopDataService
    {
        private readonly HttpClient _client;

        public ShopDataService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<ShopInfo>> GetListofShopsByCityName(string cityName)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return new List<ShopInfo>
            {
                new ShopInfo() { ShopName = "test"},
                new ShopInfo()
            };
        }

        public async Task<List<ShopInfo>> GetShopsByCityId(CancellationToken cancellationToken, int zipcityid = 0)
        {
            string key = $"GetShopsByCityId::{zipcityid}";
            var policy = Policy.Handle<Exception>().RetryAsync(Constants.PolicyRetryCount,
                async (Exception exception, int attempt) =>
                {
                    if (exception.GetType() == typeof(System.Net.WebException))
                    {
                        var error = (System.Net.WebException)exception;
                        switch (error.Status)
                        {
                            case System.Net.WebExceptionStatus.ConnectFailure:
                                break;
                        }
                    }
                    //if we fall over x number of times get data from the cache
                    if (attempt == Constants.PolicyRetryCount)
                    {
                        try
                        {
                            var shops = await BlobCache.LocalMachine.GetObject<List<ShopInfo>>(key);
                            Debug.WriteLine(shops.Count);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            throw;
                        }

                    }
                });

            var shopListPolicyResponse = policy.ExecuteAsync(async () =>
               {
                   var token = await HttpClientConnector.GetBearerToken();
                   _client.BaseAddress = new Uri(Constants.ApiEndpoint);
                   _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                   return await BlobCache.LocalMachine.GetOrFetchObject(
                       key,
                       async () =>
                       {
                           using (var handle = Insights.TrackTime("GetShopsByCityId", 
                               new Dictionary<string, string> { {"zipcityid", zipcityid.ToString()}}))
                           {
                               var response = await _client.GetAsync($"/api/v1/shops/city/{zipcityid}", cancellationToken);
                               return !response.IsSuccessStatusCode ? null : JsonConvert.DeserializeObject<ShopInfoResponse>(await response.Content.ReadAsStringAsync()).Response;
                           }
                       },
                       DateTimeOffset.Now.AddHours(1));
               });

            return await shopListPolicyResponse;
        }

        public async Task<List<ShopsSearch>> GetShopsByCityName(CancellationToken cancellationToken, string cityName)
        {
            string key = $"GetShopsByCityName::{cityName}";
            var policy = Policy.Handle<Exception>().RetryAsync(Constants.PolicyRetryCount,
                 (Exception exception, int attempt) =>
                {
                    if (exception.GetType() == typeof(System.Net.WebException))
                    {
                        var error = (System.Net.WebException)exception;
                        switch (error.Status)
                        {
                            case System.Net.WebExceptionStatus.ConnectFailure:
                                break;
                        }
                    }
                    //if we fall over x number of times get data from the cache
                    if (attempt == Constants.PolicyRetryCount) { }
                });

            var shopsSearchPolicyResponse = policy.ExecuteAsync(async () =>
            {
                var token = await HttpClientConnector.GetBearerToken();
                _client.BaseAddress = new Uri(Constants.ApiEndpoint);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                return await BlobCache.LocalMachine.GetOrFetchObject(
                    key,
                    async () =>
                    {
                        using (var handle = Insights.TrackTime("GetShopsByCityName",
                            new Dictionary<string, string> {{ "cityName", cityName}}))
                        {
                            var response =
                                await _client.GetAsync($"/api/v1/locations/city/{cityName}", cancellationToken);
                            return !response.IsSuccessStatusCode
                                ? null
                                : JsonConvert.DeserializeObject<ShopsSearchResponse>(
                                    await response.Content.ReadAsStringAsync()).Response;
                        }
                    },
                    DateTimeOffset.Now.AddHours(1));
            });

            return await shopsSearchPolicyResponse;

        }
    }
}
