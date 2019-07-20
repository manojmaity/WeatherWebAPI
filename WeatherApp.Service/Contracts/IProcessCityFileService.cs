using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Service.Contracts
{
    interface IProcessCityFileService
    {
        Task<bool> ProcessCountryFile(string filePath);
    }
}
