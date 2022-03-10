using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infastructure.EF
{
    public abstract class DesignTimeFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create();
        }
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create()
        {
            
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
            string ConnectionString = builder.GetConnectionString("Default");
            if(string.IsNullOrEmpty(ConnectionString?.Trim()))
            {
                throw new InvalidOperationException("No connectionString named 'Default' was found");
            }
            var optionsBuilder = new DbContextOptionsBuilder<TContext>()
                    .UseSqlServer(ConnectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}