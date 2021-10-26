using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace TxtToPdfService
{
    public class Program
    {
        public static ConcurrentQueue<ArquivoTxt> ArquivosTxtProcessados = new();
        //public static List<ArquivoTxt> 
        public static void Main(string[] args)
        {
            var config = InitConfiguration();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(config.GetSection("Diretorio").GetSection("Caminhos").GetSection("RepositorioLog").Value, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(config => config.AddSerilog(Log.Logger));

                    services.AddHostedService<LeitorTxt>();
                    //services.AddHostedService<DeletaTxt>();
                    services.AddHostedService<EscrevePdf>();                    
                });
        
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            return config;
        }
    }
}
