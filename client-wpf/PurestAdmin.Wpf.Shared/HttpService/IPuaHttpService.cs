// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Wpf.Shared.HttpService
{
    public interface IPuaHttpService
    {
        Task DeleteAsync(string url, object data);
        Task<T> DeleteAsync<T>(string url, object data);
        Task GetAsync(string url, object data = null);
        Task<T> GetAsync<T>(string url, object data = null);
        Task PostAsync(string url, object data);
        Task<T> PostAsync<T>(string url, object data);
        Task PutAsync(string url, object data);
        Task<T> PutAsync<T>(string url, object data);
    }
}