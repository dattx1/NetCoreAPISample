using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.ConfigurationManage;
using DataAccess.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.AuthozirationAttributes;
using Security.Extension;
using Security.CustomAuthorization;
using Microsoft.AspNetCore.Authorization;
using NotificationLayer;
using BusinessAccess.Repository;
using DataAccess.Model;
using BusinessAccess.DataContract;
using AutoMapper;
using BusinessAccess.Services.Interface;
using BusinessAccess.Services.Implement;
using Security;

namespace SampleNetCoreAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            // Configuration = builder.Build();
            var connectionStringConfig = builder.Build();

            ///ADd Config From Database
            var config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables().AddEntityFrameworkConfig(options =>
                    options.UseMySQL(connectionStringConfig.GetConnectionString("MySqlConnection"))
                 );

            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add connection string to EF

            var sqlConnectionString = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<SampleNetCoreAPIContext>(options => options.UseMySQL(sqlConnectionString));

            #endregion

            #region ADd Configuration to dependency injection

            services.AddSingleton<IConfiguration>(Configuration);

            #endregion

            #region Add Authorization by using JWT
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = Configuration["TokenAuthentication:siteUrl"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Constant.SecretSercurityKey)),

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["TokenAuthentication:siteUrl"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["TokenAuthentication:siteUrl"],

                    ValidateLifetime = true,
                };
            });
            #endregion

            #region Create Authorization Role

            var userRoleTypes = Enum.GetValues(typeof(UserTypeEnum)).Cast<UserTypeEnum>().ToList();

            for (int i = 1; i <= userRoleTypes.Count(); i++)
            {
                foreach (var policyNames in userRoleTypes.Combinate(i))
                {
                    ///Administrator,Customer
                    var policyConcat = string.Join(",", policyNames);
                    var result = policyNames.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => new { charName = c.Key, charCount = c.Count() });
                    if (result.Count() <= 0)
                    {
                        services.AddAuthorization(options =>
                        {
                            options.AddPolicy(policyConcat, policy => policy.Requirements.Add(new CustomAuthoRequire(policyConcat)));
                        });
                    }
                }
            }
            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandle>();
            #endregion

            #region Add Service to dependency injection
            services.AddTransient<IEmailProvider, EmailProvider>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IAuthozirationUtility, AuthozirationUtility>();
            #endregion

            #region Add Repository

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #endregion

            #region Add AutoMapper
            ConfigAutoMapper(services);
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }

        public void ConfigAutoMapper(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blog, BlogContract>();
              
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
