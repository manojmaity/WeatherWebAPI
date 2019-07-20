using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.Service.Contracts;
using WeatherApp.Service.Helper;

namespace WeatherApp.Service.Implementations
{
    //https://samples.openweathermap.org/data/2.5/weather?id=2172797&appid=aa69195559bd4f88d79f9aadeb77a8f6

    public class WeatherService : IWeatherService
    {
        public async Task<RootObject> GetWeatherDataUsingCityId(UInt16 cityId)
        {
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("id", Convert.ToString(cityId));
            RootObject data = await WebAPIHelper.GetAsync<RootObject>(string.Empty, parameter);
            return data;
        }
    }
}
