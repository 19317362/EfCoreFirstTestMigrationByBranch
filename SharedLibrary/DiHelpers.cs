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

            var configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional : true)
#if TST
                               .AddJsonFile($"appsettings.Tst.json", optional : true)
#elif UAT
                               .AddJsonFile($"appsettings.Uat.json", optional : true)
#elif RELEASE
                               .AddJsonFile($"appsettings.Release.json", optional : true)
#else
                               .AddJsonFile($"appsettings.Debug.json", optional : true)
#endif
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