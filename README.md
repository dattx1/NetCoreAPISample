Update Starup.cs Constructor to 

```
 public Startup(IHostingEnvironment env)
  {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      Configuration = builder.Build();

      //var connectionStringConfig = builder.Build();

      /////ADd Config From Database
      //var config = new ConfigurationBuilder()
      //    .SetBasePath(env.ContentRootPath)
      //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
      //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
      //    .AddEnvironmentVariables().AddEntityFrameworkConfig(options =>
      //        options.UseMySQL(connectionStringConfig.GetConnectionString("MySqlConnection"))
      //     );

      //Configuration = config.Build();
  }
```    

Open Pakage Manager Console and type:
```
 - update-database
 ```
 
 
 Update Startup.cs to
 
 ```
public Startup(IHostingEnvironment env)
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

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
```
