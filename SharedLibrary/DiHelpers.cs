using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedLibrary
{
    public static class DiHelpers
    {
        private static readonly ServiceProvider _provider;

        static DiHelpers()
        {
            var serviceCollection = new ServiceCollection();

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = "Debug";
            }

            var configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true)
                               .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                               .AddEnvironmentVariables()
                               .Build();

            serviceCollection.AddSingleton(_ => configuration);

            _provider = serviceCollection.BuildServiceProvider();
        }

        public static T DiFactory<T>()
        {
            return _provider.GetService<T>();
        }
    }
}