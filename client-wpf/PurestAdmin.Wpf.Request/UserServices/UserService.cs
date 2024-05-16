// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Wpf.Request.UserServices.Dtos;
using PurestAdmin.Wpf.Shared.CommonRequest;
using PurestAdmin.Wpf.Shared.HttpService;

namespace PurestAdmin.Wpf.Request.UserServices
{
    public class UserService(IPuaHttpService httpService)
    {
        private readonly IPuaHttpService _httpService = httpService;

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedList<UserOutput>> GetPagedListAsync(GetPagedListInput input)
        {
            return await _httpService.GetAsync<PagedList<UserOutput>>("/user/paged-list", input);
        }
    }
}
