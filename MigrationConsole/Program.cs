using System;
using Microsoft.EntityFrameworkCore;

namespace MigrationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Migrations");

            using (var dbContext = new TestCompanyContextFactory().CreateDbContext(args))
            {
                dbContext.Database.Migrate();
            }
            
            Console.WriteLine("Migrations Finished");
        }
    }
}