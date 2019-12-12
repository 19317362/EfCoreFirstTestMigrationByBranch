using System;
using System.IO;
using EfCoreFirstTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharedLibrary;

namespace MigrationConsole
{
    public class TestCompanyContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestCompanyContextFactory()
        {
        }

        public TestDbContext CreateDbContext(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                throw new Exception("Not Provider");
            }

            var connectionString = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile($"appsettings.{args[0].ToLower()}.json", optional : true)
                                  .Build()
                                  .GetConnectionString("TestCompany");

            var optionBuilder = new DbContextOptionsBuilder<TestDbContext>()
               .UseSqlServer(connectionString
                           , builder =>
                             {
                                 builder.CommandTimeout(2400);
                                 builder.EnableRetryOnFailure(2);
                                 builder.MigrationsHistoryTable("_MigrationsHistory", "dbo");
                                 builder.MigrationsAssembly("MigrationConsole");
                             });

            return new TestDbContext(optionBuilder.Options);
        }
    }
}