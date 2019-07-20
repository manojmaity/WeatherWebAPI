using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.API.Controllers;
using WeatherApp.Service.Implementations;

namespace WeatherApp.API.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public async void Get()
        {
            //Arrange
            WeatherService service = new WeatherService();
            WeatherController controller = new WeatherController(service);

            //Act
             //var msg = await controller.Get(1);

            //Task.Delay(100).Wait();
            //Assert
            //Assert.IsNotNull(msg);

           await Task.Run(async () =>
            {
                var msg = await controller.Get(1);
                Assert.IsNotNull(msg);
            });


        }
    }
}
