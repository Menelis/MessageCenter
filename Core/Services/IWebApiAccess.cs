using System.Threading.Tasks;
using System.Collections.Generic;
namespace Core.Services
{
    public interface IWebApiAccess
    {
        /// <summary>
        /// Returns data from Api End Point for GET method
        /// </summary>
        /// <param name="apiUrlEndPoint">Api End Point</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
         Task<IList<T>> GetData<T>(string apiUrlEndPoint);
    }
}