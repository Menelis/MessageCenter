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
            var employees = await _webApiAccess.GetData<Employee>(_applicationSettings.ApiEndPointUrl);
            return employees.Where(employee => employee.ItsYourBirthDay)
                            .ToList();
        }
    }
}