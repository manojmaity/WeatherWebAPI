using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherApp.Service.Implementations;

namespace WeatherApp.API.Controllers
{
    public class CityWeatherController : ApiController
    {
        private ProcessFileService _pService;

        public CityWeatherController(ProcessFileService cityFileService)
        {
            _pService = cityFileService;
        }

        /// <summary>
        ///     Fetch the weather for particular city using id
        /// </summary>
        /// <param name="cityId">
        ///     Id of the city
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<HttpResponseMessage> Get(string fileName)
        {
            string mapPath = $"{AppDomain.CurrentDomain.BaseDirectory}CityList\\{fileName}";
            var data = await _pService.ProcessCountryFile(mapPath);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
