// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Wpf.Request.AuthServices.Dots;
using PurestAdmin.Wpf.Shared.HttpService;

namespace PurestAdmin.Wpf.Request.AuthServices
{
    public class AuthService(IPuaHttpService httpService)
    {
        private readonly IPuaHttpService _httpService = httpService;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return await _httpService.PostAsync<LoginOutput>("/auth/login", input);
        }
    }
}
