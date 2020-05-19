using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using StorageManager.Interfaces;
using StorageManager.Services;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace StorageManager
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<LoggerFactory>()
                    .As<ILoggerFactory>()
                    .SingleInstance()
                    .AutoActivate();

                builder.RegisterGeneric(typeof(Logger<>))
                    .As(typeof(ILogger<>))
                    .SingleInstance();

                builder.RegisterType<Configuration>().As<IConfiguration>().SingleInstance();
                builder.RegisterType<StorageRepository>().As<IStorageRepository>();
                builder.RegisterType<Startup>().As<IStartup>();
                builder.RegisterType<FileReader>().As<IFileReader>();
                builder.RegisterType<DataLoader>().As<IDataLoader>();
                builder.RegisterType<LineItemConverter>().As<ILineItemConverter>();
                builder.RegisterType<ConsoleDataPresenter>().As<IDataPresenter>();
                builder.RegisterType<OutputFormatter>().As<IOutputFormatter>();

                using (var container = builder.Build())
                {
                    var loggerFactory = container.Resolve<ILoggerFactory>();
                    loggerFactory.AddSerilog();

                    if (args.Length > 0)
                    {
                        var config = container.Resolve<IConfiguration>();
                        config.FilePath = args[0];
                    }

                    var startup = container.Resolve<IStartup>();
                    await startup.RunAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }

            Console.ReadLine();
        }
    }
}