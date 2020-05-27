using AGLPetLister.Services;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AGLPetLister
{
    /// <summary>
    /// This setup the Dependency Injection module
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
                        
            serviceProvider.GetService<DisplayApp>().Run();
        }

        /// <summary>
        /// Intiate the ServiceCollection
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Setup the Configuration file
        /// </summary>
        /// <returns></returns>
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

    }
}
