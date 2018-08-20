using Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Security.AuthozirationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorization : Attribute, IAuthorizeData
    {
        public CustomAuthorization(params object[] Roles)
        {
            if (Roles.Any(p => p.GetType().BaseType != typeof(Enum)))
            {
                this.Policy = Enum.GetName(typeof(UserTypeEnum), UserTypeEnum.Administrator);
            }
            else
            {
                this.Policy = string.Join(",", Roles.Select(p => Enum.GetName(p.GetType(), p)).Distinct().ToList());
            }
        }

        public string Policy { get; set; }
        public string Roles { get; set; }
        public string AuthenticationSchemes { get; set; }
    }
}
