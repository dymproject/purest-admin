// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Configuration;
using System.Net;

using Flurl.Http;
using Flurl.Http.Configuration;

using Microsoft.Extensions.Caching.Memory;


namespace PurestAdmin.Wpf.Shared.HttpService
{
    public class PuaHttpService : IPuaHttpService
    {
        private readonly IFlurlClient _flurlClient;
        public PuaHttpService(IFlurlClientCache clients, IMemoryCache memoryCache)
        {
            _flurlClient = clients.Get("pua");
            _flurlClient.BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
            _flurlClient.AfterCall((action) =>
            {
                if (action.Succeeded)
                {
                    action.Response.Headers.TryGetFirst("accesstoken", out string accessToken);
                    if (accessToken != null)
                    {
                        memoryCache.Set("Access-Token", accessToken);
                    }
                }
            });
            _flurlClient.BeforeCall((action) =>
            {
                var token = memoryCache.Get("Access-Token");
                action.Request.Headers.Add("Authorization", $"Bearer {token}");
            });
            _flurlClient.OnError(async (action) =>
            {
                if (action.Response != null && action.Response.StatusCode == (int)HttpStatusCode.BadRequest && action.Response.ResponseMessage.Content != null)
                {
                    var errorMsg = await action.Response.ResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception(errorMsg);
                }
                throw action.Exception;
            });
        }
        public async Task GetAsync(string url, object data = null)
        {
            await _flurlClient.Request(url).SetQueryParams(data).GetAsync();
        }
        public async Task<T> GetAsync<T>(string url, object data = null)
        {
            return await _flurlClient.Request(url).SetQueryParams(data).GetJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            return await _flurlClient.Request(url).PostJsonAsync(data).ReceiveJson<T>();
        }
        public async Task PostAsync(string url, object data)
        {
            await _flurlClient.Request(url).PostJsonAsync(data);
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            return await _flurlClient.Request(url).PutJsonAsync(data).ReceiveJson<T>();
        }
        public async Task PutAsync(string url, object data)
        {
            await _flurlClient.Request(url).PutJsonAsync(data);
        }

        public async Task<T> DeleteAsync<T>(string url, object data)
        {
            return await _flurlClient.Request(url).SetQueryParams(data).DeleteAsync().ReceiveJson<T>();
        }
        public async Task DeleteAsync(string url, object data)
        {
            await _flurlClient.Request(url).SetQueryParams(data).DeleteAsync();
        }
    }
}
