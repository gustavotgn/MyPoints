using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyPoints.Identity.Data;
using MyPoints.Identity.Extensions;
using Serilog;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MyPoints.Identity.Data.Seeds;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using MyPoints.Identity.Domain.Entities;

namespace MyPoints.Identity
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);
                host.MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<SeedData>>();

                    using (var userManager = services.GetService<UserManager<ApplicationUser>>())
                    {
                        SeedData.EnsureSeedData(context);
                        if (env.EnvironmentName.ToLower() == "development")
                        {
                            SeedData.EnsureSeedDataDev(context,userManager);
                        }
                    };


                });

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .Build();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }


        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();


            return builder.Build();
        }
    }
}
