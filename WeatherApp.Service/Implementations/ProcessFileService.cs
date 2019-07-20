using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.Service.Contracts;

namespace WeatherApp.Service.Implementations
{
    public class ProcessFileService : IProcessCityFileService
    {
        private const string outputDir = "Data";

        #region Public Methods
        public async Task<bool> ProcessCountryFile(string filePath)
        {
            WeatherService wthrService = new WeatherService();
            try
            {
                string filedata = File.ReadAllText(filePath);
                List<CityList> cities = JsonConvert.DeserializeObject<List<CityList>>(filedata);
                foreach (CityList city in cities)
                {
                    RootObject data = await wthrService.GetWeatherDataUsingCityId(city.Id);
                    SaveCountryDataByDate(DateTime.Today, city, data);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        #endregion

        #region Private Methods
        private bool SaveCountryDataByDate(DateTime date, CityList city, RootObject data)
        {
            bool result;
            result = CreateOutputFolder();

            if (result)
            {
                result = CreateCityFolder(city);
            }

            if (result)
            {
                result = CreateCityFile(date, city, data);
            }
            return result;
        }

        private bool CreateOutputFolder()
        {
            string baseDir = $"{AppDomain.CurrentDomain.BaseDirectory}{outputDir}";
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            return true;
        }

        private bool CreateCityFolder(CityList city)
        {
            string cityDir = $"{AppDomain.CurrentDomain.BaseDirectory}{outputDir}\\{city.Id}_{city.Name}";
            if (!Directory.Exists(cityDir))
            {
                Directory.CreateDirectory(cityDir);
            }
            return true;
        }

        private bool CreateCityFile(DateTime date, CityList city, RootObject data)
        {
            string cityFile = $"{AppDomain.CurrentDomain.BaseDirectory}{outputDir}\\{city.Id}_{city.Name}\\{date.ToString("dd-MMM-yyyy")}_{city.Id}.json";
            if (!File.Exists(cityFile))
            {
                using (StreamWriter sw = File.CreateText(cityFile))
                {
                    sw.Write(JsonConvert.SerializeObject(data));
                }
            }
            return true;
        }
        #endregion
    }
}
