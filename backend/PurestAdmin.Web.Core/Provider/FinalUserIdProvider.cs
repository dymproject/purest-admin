// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System.Security.Claims;

using Microsoft.AspNetCore.SignalR;

using PurestAdmin.Core.Const;

namespace PurestAdmin.Web.Core.Provider;
public class FinalUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirstValue(ClaimConst.USERID);
    }
}
