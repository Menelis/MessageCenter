using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Services;
using Core.Gateways.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Core.Helpers;

namespace MessageCenterService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    IRunningYearRepository _runningYearRepository = scope.ServiceProvider.GetRequiredService<IRunningYearRepository>();
                    IEmployeeService _employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
                    IBirthDaySentLogRepository _birthDaySentLogRepository = scope.ServiceProvider.GetRequiredService<IBirthDaySentLogRepository>();
                    IEmailSender _emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                    ApplicationSettings applicationSettings = (scope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>()).Value;
                    // Check if the service has ran on the current year
                    Guid runningYearId = await GetRunningYearId(_runningYearRepository);
                    // if the Service hasn't ran(runningYearId is empty) on the current then save the current year return id else use the id
                    runningYearId = runningYearId == Guid.Empty ? await SaveRunningYear(_runningYearRepository) : runningYearId;

                    foreach(var employee in await _employeeService.GetEmployeeBirthDays())
                    {
                        if (!await HasMessageBeenSent(employee.Id, runningYearId, _birthDaySentLogRepository))
                        {
                            var to = new[] { employee.Email ?? applicationSettings.Email };
                            string content = $"Happy Birthday {employee.Name} {employee.LastName}";
                            await _emailSender.SendEmail(new Message(to, "Birthday Wish", content));

                            await SaveBirthDayMessageSent(employee.Id, runningYearId, _birthDaySentLogRepository);
                        }
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private static async Task<Guid> GetRunningYearId(IRunningYearRepository runningYearRepository)
        {
            var runningYear = await runningYearRepository.Get(year => year.Year == DateTime.Now.Year);
            if (runningYear == null) return Guid.Empty;
            return runningYear.Id;
        }
        private static async Task<bool> HasMessageBeenSent(int employeeId, Guid runningYearId, IBirthDaySentLogRepository birthDaySentLogRepository)
        {
            var birthDayMessageHasBeenSent = await birthDaySentLogRepository.Get(message => message.EmployeeId == employeeId && message.RunningYearId == runningYearId);
            return birthDayMessageHasBeenSent != null;
        }
        private static async Task<Guid> SaveRunningYear(IRunningYearRepository runningYearRepository)
        {
           var runningYear = await runningYearRepository.Add(new Core.Entities.RunningYear { Year = DateTime.Now.Year });
            return runningYear.Id;
        }
        private static async Task SaveBirthDayMessageSent(int employerId, Guid runningYearId, IBirthDaySentLogRepository birthDaySentLogRepository)
        {
            await birthDaySentLogRepository.Add(new Core.Entities.BirthDaySentLog
            {
                EmployeeId = employerId,
                RunningYearId = runningYearId
            });
        }
    }
}
