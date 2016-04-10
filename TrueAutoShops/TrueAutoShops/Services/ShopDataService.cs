using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrueAutoShops.Helpers;
using TrueAutoShops.Models;

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

        public async Task<ShopInfo> GetShopsByCityId(CancellationToken cancellationToken, int zipcityid = 0)
        {
            try
            {

           
            var token = await HttpClientConnector.GetBearerToken();
            _client.BaseAddress = new Uri("http://asapservices.cloudapp.net:8080");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var response = await _client.GetAsync($"/api/v1/shops/city/{zipcityid}", cancellationToken);
           // if (!response.IsSuccessStatusCode) { return null; }

            var jsonMessage = await response.Content.ReadAsStringAsync();

            var shopInfo = JsonConvert.DeserializeObject<dynamic>(jsonMessage);

            return shopInfo.First();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
