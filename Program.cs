using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      
      if (args.Length > 0 && args[0].ToLower() == "/seed")
      {
        RunSeeding(host);
        return;
      }
      
      host.Run();
    }

    private static void RunSeeding(IHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
      using(var scope = scopeFactory.CreateScope())
      {
          var seeder = scope.ServiceProvider.GetService<OpenERPSeeder>();
          seeder.SeedAsync().Wait();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(AddConfiguration)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });

        private static void AddConfiguration(HostBuilderContext context, 
        IConfigurationBuilder builder)
        {
            builder.Sources.Clear();
            builder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("config.json")
              .AddEnvironmentVariables();
        }
    }
}
