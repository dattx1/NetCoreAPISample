using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using DataAccess.DBContext;

namespace DataAccess.ConfigurationManage
{
    public class AddSystemConfig
    {
        public static Dictionary<string, string> Initialize(IApplicationBuilder applicationBuilder)//LetgoSysContext is EF context
        {
            var scopeFactory = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SampleNetCoreAPIContext>();

                context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

                var configs = context.SampleNetCoreConfig.ToList();
                var dict = new Dictionary<string, string>();
                foreach (var item in configs)
                {
                    dict[item.Key] = item.Value;
                };

                return dict;
            }
        }
    }
}
