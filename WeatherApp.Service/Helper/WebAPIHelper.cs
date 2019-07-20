using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeatherApp.Service.Helper
{
    public class WebAPIHelper
    {
        private const string _baseUrl = "https://samples.openweathermap.org/data/2.5/weather";
        public const string _apiKey = "aa69195559bd4f88d79f9aadeb77a8f6";

        public static HttpClient GetClient()
        {
            HttpClient _client =
               new HttpClient()
               {
                   Timeout = new TimeSpan(0, 1, 0)
               };
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        public static async Task<X> GetAsync<X>(string ResourceURI, Dictionary<string, string> Parameters)
        {
            Exception excep;
            try
            {
                HttpClient client = GetClient();
                string URI = $"{ToQueryString(ResourceURI, Parameters)}&appid={_apiKey}";
                using (HttpResponseMessage response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<X>();
                    }
                    else
                    {
                        excep = HandleErrorResponse(response);
                    }

                    return default(X);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static Exception HandleErrorResponse(HttpResponseMessage response)
        {
            Exception excep = null;
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.ProxyAuthenticationRequired:
                    case HttpStatusCode.RequestTimeout:
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:

                        try
                        {
                            excep = response.Content.ReadAsAsync<Exception>().Result;
                        }
                        catch (Exception)
                        {
                            string error = response.Content.ReadAsStringAsync().Result;
                            excep = new Exception(error);
                        }
                        break;
                    default:
                        string err = response.Content.ReadAsStringAsync().Result;
                        excep = new Exception(err);
                        break;
                }
            }
            catch (Exception)
            {
                excep = new Exception("Unable to complete the request. View Api log for more details.");
            }

            return excep;
        }

        private static string ToQueryString(string Query, Dictionary<string, string> nvc)
        {
            var array = (from param in nvc
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(param.Key), HttpUtility.UrlEncode(param.Value)))
                .ToArray();

            return String.Format("{0}{1}{2}", Query, "?", string.Join("&", array));
        }
    }
}
