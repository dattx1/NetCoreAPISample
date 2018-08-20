using Common;
using Microsoft.AspNetCore.Authorization;
using Security.SecurityModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Security
{
    public interface IAuthozirationUtility
    {
        string RenderAccessToken(current_user_access access_user);
        JwtSecurityToken GetRequestAccessToken(AuthorizationHandlerContext context);
    }
}
