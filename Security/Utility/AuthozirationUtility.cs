using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Security.SecurityModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Security
{
    public class AuthozirationUtility : IAuthozirationUtility
    {
        private readonly IConfiguration _configuration;

        public AuthozirationUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RenderAccessToken(current_user_access access_user)
        {
            var audience = _configuration["TokenAuthentication:siteUrl"];

            var jwtToken = new JwtSecurityToken(
                issuer: audience,
                audience: audience,
                claims: GetTokenClaims(access_user),
                expires: access_user.ExpireTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constant.SecretSercurityKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }


        public JwtSecurityToken GetRequestAccessToken(AuthorizationHandlerContext context)
        {
            try
            {
                var token = GetToken(context);
                token = token.Replace("Bearer ", "");
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            }
            catch
            {
                return null;
            }
        }

        private string GetToken(AuthorizationHandlerContext context)
        {
            var httpContext = ((Microsoft.AspNetCore.Mvc.ActionContext)context.Resource).HttpContext;
            var token = httpContext.Request.Headers["Authorization"].ToString();

            return string.IsNullOrWhiteSpace(token) ? string.Empty : token;
        }

        private IEnumerable<Claim> GetTokenClaims(current_user_access access_user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, access_user.Email),
                new Claim(JwtRegisteredClaimNames.Typ, string.Join(",",access_user.UserType)),
                new Claim(JwtRegisteredClaimNames.GivenName, access_user.UserName),
                new Claim(JwtRegisteredClaimNames.Exp, access_user.ExpireTime.ToShortDateString())
            };
        }

    }
}
