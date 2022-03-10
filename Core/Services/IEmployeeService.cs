using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Entities;
namespace Core.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Returns a list of employees who's celebrating their birthdays today
        /// </summary>
        /// <returns></returns>
         Task<IEnumerable<Employee>> GetEmployeeBirthDays();
    }
}