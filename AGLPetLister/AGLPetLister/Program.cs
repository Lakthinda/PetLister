using AGLPetLister.Services;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AGLPetLister
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
                        
            serviceProvider.GetService<DisplayApp>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            services.AddHttpClient();

            // Add logs
            //services.AddSingleton<ILogger>();

            // Add Services
            services.AddTransient<DisplayApp>();
            services.AddTransient<IPetAPIService, PetAPIService>();
            services.AddTransient<PetListerService>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

    }
}
