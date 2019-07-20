using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherApp.Model;
using WeatherApp.Service.Implementations;

namespace WeatherApp.API.Controllers
{
    public class WeatherController : ApiController
    {
        private WeatherService _wService;

        public WeatherController(WeatherService weatherService)
        {
            _wService = weatherService;
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
        public async Task<HttpResponseMessage> Get(ushort cityId)
        {
            RootObject data = await _wService.GetWeatherDataUsingCityId(cityId);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
       
    }
}
