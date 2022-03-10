using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Infastructure.Extensions;
using Core.Helpers;

namespace MessageCenterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.ConfigureDbContext(hostContext.Configuration);
                    services.RegisterRepositories();
                    services.AddTransient<IWebApiAccess, WebApiAccess>();
                    services.AddTransient<IEmployeeService, EmployeeService>();
                    services.AddTransient<IEmailSender, EmailSender>();
                    services.Configure<ApplicationSettings>(hostContext.Configuration.GetSection(nameof(ApplicationSettings)));
                    services.Configure<EmailSettings>(hostContext.Configuration.GetSection(nameof(EmailSettings)));
                }).UseWindowsService();
    }
}
