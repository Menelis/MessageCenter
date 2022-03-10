using Infastructure.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Core.Gateways.Repositories;
using Infastructure.Gateways.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infastructure.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Db Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionStringName"></param>
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration, string connectionStringName = "Default")
        {
            services.AddDbContext<MessageCenterDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        }
        /// <summary>
        /// Register Repositories
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRunningYearRepository, RunningYearRepository>();
            services.AddScoped<IBirthDaySentLogRepository, BirthDaySentLogRepository>();
        }
    
    }
}