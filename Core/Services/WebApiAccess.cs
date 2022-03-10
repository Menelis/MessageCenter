using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Services
{
    public class WebApiAccess : IWebApiAccess
    {
        public async Task<IList<T>> GetData<T>(string apiUrlEndPoint)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrlEndPoint);
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IList<T>>(await response.Content.ReadAsStringAsync());
            }
            return new List<T>();
        }

    }
}