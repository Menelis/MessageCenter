using Core.Entities;
using Core.Helpers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IWebApiAccess _webApiAccess;
        private readonly ApplicationSettings _applicationSettings;

        public EmployeeService(IWebApiAccess webApiAccess, IOptions<ApplicationSettings> options)
        {
            _webApiAccess = webApiAccess;
            _applicationSettings = options.Value;
        }
        public async Task<IEnumerable<Employee>> GetEmployeeBirthDays()
        {
            var employees = await _webApiAccess.GetData<Employee>($"{_applicationSettings.ApiEndPointUrl}/employees");
            var excludedEmployeesIds = await GetExcludedEmployeesIds();           
            return employees.Where(employee => employee.ItsYourBirthDay && !excludedEmployeesIds.Contains(employee.Id))
                            .ToList();
        }

        public async Task<IEnumerable<int>> GetExcludedEmployeesIds()
        {
            return await _webApiAccess.GetData<int>($"{_applicationSettings.ApiEndPointUrl}/do-not-send-birthday-wishes");
        }
    }
}