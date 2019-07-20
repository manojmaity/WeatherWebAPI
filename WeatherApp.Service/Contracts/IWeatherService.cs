using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.Service.Contracts
{
    interface IWeatherService
    {
        Task<RootObject> GetWeatherDataUsingCityId(UInt16 countryId);
    }
}
