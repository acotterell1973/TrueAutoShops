using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrueAutoShops.Models;
using TrueAutoShops.Models.Response;
using Xamarin;

namespace TrueAutoShops.Services
{
    public class SecurityDataService : ISecurityDataService, IDisposable
    {
        private HttpClient _client;

        public SecurityDataService(HttpClient client)
        {
            _client = client;
        }
        public async Task<TokenResponseModel> GetTokenTask()
        {
          //  await Task.Delay(TimeSpan.FromSeconds(1));
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


            var response = await _client.PostAsync("/oauth2/token", content);
            if (!response.IsSuccessStatusCode) { return null; }

            string jsonMessage;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                jsonMessage = new StreamReader(responseStream).ReadToEnd();
            }

            var tokenResponse = (TokenResponseModel)JsonConvert.DeserializeObject(jsonMessage, typeof(TokenResponseModel));

            return tokenResponse;


        }

        public async Task<TokenResponseModel> LoginUser(CancellationToken cancellationToken,Login login)
        {
            var postDictionary = (from x in login.GetType().GetRuntimeProperties() select x)
                .ToDictionary(x => x.Name, 
                x => x.GetMethod.Invoke(login, null) == null ? "" : x.GetMethod.Invoke(login, null).ToString());
            var content = new FormUrlEncodedContent(postDictionary);

            using (var handle = Insights.TrackTime("RegisterUser", postDictionary))
            {
                var response =
                    await _client.PostAsync("/api/audience/authenticate?returnUrl=test", content, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var jsonMessage = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(jsonMessage);

                return tokenResponse;
            }

        }

        public async Task<CreateProfileResponse> RegisterUser(CancellationToken cancellationToken, UserProfile user)
        {
            var postDictionary = (from x in user.GetType().GetRuntimeProperties() select x)
                   .ToDictionary(x => x.Name,
                   x => x.GetMethod.Invoke(user, null) == null ? "" : x.GetMethod.Invoke(user, null).ToString());
            var content = new FormUrlEncodedContent(postDictionary);

            using (var handle = Insights.TrackTime("RegisterUser", postDictionary))
            {
                var response = await _client.PostAsync("/api/audience/createprofile", content, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var jsonMessage = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonConvert.DeserializeObject<CreateProfileResponse>(jsonMessage);

                return tokenResponse;
            }
        }

        public void Dispose()
        {
            _client = null;
        }
    }
}