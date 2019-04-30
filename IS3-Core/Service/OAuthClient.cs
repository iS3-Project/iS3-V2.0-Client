using iS3.Core.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core
{
    public class OAuthClient
    {
        private HttpClient _httpClient;
        private string token;

        public OAuthClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }

        public async Task<string> GetAccessToken(string username, string password)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", username);
            parameters.Add("password", password);

            var response = await _httpClient.PostAsync("token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseValue)["access_token"].Value<string>();
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> Call_WebAPI_By_Resource_Owner_Password_Credentials_Grant(string username, string password)
        {
            if (string.IsNullOrEmpty(token))
                token = await GetAccessToken(username, password);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        public async Task<string> GetByAuth(string url)
        {
            if (ServiceConfig.NeedCache)
            {
                //缓存判断
                if (iS3Cache.checkIfExist(url))
                {
                    if (iS3Cache.CheckIfLastet(url))
                    {
                        return iS3Cache.GetFromCache(url);
                    }
                }
            }
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            if (ServiceConfig.NeedCache)
            {
                //缓存保存
                iS3Cache.SaveToCache(url, result);
            }
            return result;
        }

        public async Task<string> PostByAuth(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(body));
            return response.Content.ReadAsStringAsync().Result;
        }
        public async Task<string> PutByAuth(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(url, new StringContent(body));
            return response.Content.ReadAsStringAsync().Result;
        }
        public async Task<string> DeleteByAuth(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
