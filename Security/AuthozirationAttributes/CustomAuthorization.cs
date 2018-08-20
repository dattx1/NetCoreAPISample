using Common;
using Microsoft.AspNetCore.Authorization;
using Security.Extension;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;

namespace Security.CustomAuthorization
{
    public class CustomAuthoRequire : IAuthorizationRequirement
    {
        public List<UserTypeEnum> AppceptUserTypes { get; set; }

        public CustomAuthoRequire(string policyName = "")
        {
            this.AppceptUserTypes = new List<UserTypeEnum>() { UserTypeEnum.Administrator };
            AppceptUserTypes.AddRange(RoleHandle.GetRoles(policyName));
        }
    }

    public class CustomAuthorizationHandle : AuthorizationHandler<CustomAuthoRequire>
    {
        protected IAuthozirationUtility _authozirationUtility;

        public CustomAuthorizationHandle(IAuthozirationUtility authozirationUtility)
        {
            _authozirationUtility = authozirationUtility;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthoRequire requirement)
        {
            try
            {
                var jwtToken = _authozirationUtility.GetRequestAccessToken(context);
                var type = jwtToken.GetClaimValue(JwtRegisteredClaimNames.Typ.ToString());
                var userRoles = RoleHandle.GetRoles(type);

                var currentAcceptRoles = userRoles.Select(x => (int)x).FirstOrDefault(y => requirement.AppceptUserTypes.Select(z => (int)z).Contains(y));

                if (currentAcceptRoles > 0)
                    context.Succeed(requirement);
                else
                    context.Fail();

                return Task.FromResult(0);
            }
            catch
            {
                context.Fail();
                return Task.FromResult(0);
            }

        }
    }

    public static class RoleHandle
    {
        public static List<UserTypeEnum> GetRoles(string roleStrings)
        {
            var result = new List<UserTypeEnum>() { };
            try
            {
                if (!string.IsNullOrEmpty(roleStrings))
                {
                    var listRole = roleStrings.Split(",");

                    foreach (var role in listRole.Distinct().ToList())
                    {
                        result.Add((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), role));
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }

}
