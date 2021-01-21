using AccuWeatherApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccuWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration config;

        public WeatherController(IConfiguration configuration)
        {
            config = configuration;
        }

        [HttpGet("GetLocations")]
        public ActionResult<List<City>> GetLocations(string query)
        {
            if (string.IsNullOrEmpty(query)) { return new List<City>(); }
            var url = config.GetValue<string>("BaseUrl") + "locations/v1/cities/autocomplete?apikey=" + 
                config.GetValue<string>("ApiKey") + "&q=" + query;
            using(HttpClient client = new HttpClient())
            {
                var res = client.GetAsync(url);
                return JsonConvert.DeserializeObject<List<City>>(res.Result.Content.ReadAsStringAsync().Result);
                return JsonConvert.DeserializeObject<List<City>>("[{\"Version\":1,\"Key\":\"213225\",\"Type\":\"City\",\"Rank\":30,\"LocalizedName\":\"Jerusalem\",\"Country\":{\"ID\":\"IL\",\"LocalizedName\":\"Israel\"},\"AdministrativeArea\":{\"ID\":\"JM\",\"LocalizedName\":\"Jerusalem\"}},{\"Version\":1,\"Key\":\"306735\",\"Type\":\"City\",\"Rank\":42,\"LocalizedName\":\"Jerez de la Frontera\",\"Country\":{\"ID\":\"ES\",\"LocalizedName\":\"Spain\"},\"AdministrativeArea\":{\"ID\":\"AN\",\"LocalizedName\":\"Andalusia\"}},{\"Version\":1,\"Key\":\"329548\",\"Type\":\"City\",\"Rank\":45,\"LocalizedName\":\"Jersey City\",\"Country\":{\"ID\":\"US\",\"LocalizedName\":\"United States\"},\"AdministrativeArea\":{\"ID\":\"NJ\",\"LocalizedName\":\"New Jersey\"}},{\"Version\":1,\"Key\":\"465\",\"Type\":\"City\",\"Rank\":51,\"LocalizedName\":\"Jeremie\",\"Country\":{\"ID\":\"HT\",\"LocalizedName\":\"Haiti\"},\"AdministrativeArea\":{\"ID\":\"GA\",\"LocalizedName\":\"Grande Anse\"}},{\"Version\":1,\"Key\":\"224190\",\"Type\":\"City\",\"Rank\":51,\"LocalizedName\":\"Jerash\",\"Country\":{\"ID\":\"JO\",\"LocalizedName\":\"Jordan\"},\"AdministrativeArea\":{\"ID\":\"JA\",\"LocalizedName\":\"Jerash\"}},{\"Version\":1,\"Key\":\"244895\",\"Type\":\"City\",\"Rank\":55,\"LocalizedName\":\"Jerada\",\"Country\":{\"ID\":\"MA\",\"LocalizedName\":\"Morocco\"},\"AdministrativeArea\":{\"ID\":\"02\",\"LocalizedName\":\"l'Oriental\"}},{\"Version\":1,\"Key\":\"232887\",\"Type\":\"City\",\"Rank\":55,\"LocalizedName\":\"Jerécuaro\",\"Country\":{\"ID\":\"MX\",\"LocalizedName\":\"Mexico\"},\"AdministrativeArea\":{\"ID\":\"GUA\",\"LocalizedName\":\"Guanajuato\"}},{\"Version\":1,\"Key\":\"3558846\",\"Type\":\"City\",\"Rank\":55,\"LocalizedName\":\"Jerez\",\"Country\":{\"ID\":\"MX\",\"LocalizedName\":\"Mexico\"},\"AdministrativeArea\":{\"ID\":\"ZAC\",\"LocalizedName\":\"Zacatecas\"}},{\"Version\":1,\"Key\":\"236610\",\"Type\":\"City\",\"Rank\":55,\"LocalizedName\":\"Jerez de García Salinas\",\"Country\":{\"ID\":\"MX\",\"LocalizedName\":\"Mexico\"},\"AdministrativeArea\":{\"ID\":\"ZAC\",\"LocalizedName\":\"Zacatecas\"}},{\"Version\":1,\"Key\":\"313422\",\"Type\":\"City\",\"Rank\":65,\"LocalizedName\":\"Jerablus\",\"Country\":{\"ID\":\"SY\",\"LocalizedName\":\"Syria\"},\"AdministrativeArea\":{\"ID\":\"HL\",\"LocalizedName\":\"Aleppo\"}}]");
            }
        }

        [HttpGet("GetCurrentCondition")]
        public ActionResult<List<CurrentCondition>> GetCurrentCondition(int cityKey)
        {
            var url = config.GetValue<string>("BaseUrl") + "currentconditions/v1/"
                + cityKey + "?apikey=" + config.GetValue<string>("ApiKey");
            using (HttpClient client = new HttpClient())
            {
                var res = client.GetAsync(url);
                return JsonConvert.DeserializeObject<List<CurrentCondition>>(res.Result.Content.ReadAsStringAsync().Result);
                return JsonConvert.DeserializeObject<List<CurrentCondition>>("[{\"LocalObservationDateTime\":\"2021-01-21T00:56:00+02:00\",\"EpochTime\":1611183360,\"WeatherText\":\"Mostly clear\",\"WeatherIcon\":34,\"HasPrecipitation\":false,\"PrecipitationType\":null,\"IsDayTime\":false,\"Temperature\":{\"Metric\":{\"Value\":12.9,\"Unit\":\"C\",\"UnitType\":17},\"Imperial\":{\"Value\":55,\"Unit\":\"F\",\"UnitType\":18}},\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/current-weather/215854?lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/current-weather/215854?lang=en-us\"}]");
            }
        }

        [HttpGet("GetForecast")]
        public ActionResult<Forecast> GetForecast(int cityKey)
        {
            var url = config.GetValue<string>("BaseUrl") + "forecasts/v1/daily/5day/"
                + cityKey + "?apikey=" + config.GetValue<string>("ApiKey");
            using (HttpClient client = new HttpClient())
            {
                var res = client.GetAsync(url);
                return JsonConvert.DeserializeObject<Forecast>(res.Result.Content.ReadAsStringAsync().Result);
                return JsonConvert.DeserializeObject<Forecast>("{\"Headline\":{\"EffectiveDate\":\"2021 - 01 - 23T07: 00:00 + 02:00\",\"EffectiveEpochDate\":1611378000,\"Severity\":4,\"Text\":\"Pleasant this weekend\",\"Category\":\"mild\",\"EndDate\":null,\"EndEpochDate\":null,\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/extended-weather-forecast/215854?lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?lang=en-us\"},\"DailyForecasts\":[{\"Date\":\"2021-01-20T07:00:00+02:00\",\"EpochDate\":1611118800,\"Temperature\":{\"Minimum\":{\"Value\":37,\"Unit\":\"F\",\"UnitType\":18},\"Maximum\":{\"Value\":59,\"Unit\":\"F\",\"UnitType\":18}},\"Day\":{\"Icon\":13,\"IconPhrase\":\"Mostly cloudy w/ showers\",\"HasPrecipitation\":true,\"PrecipitationType\":\"Rain\",\"PrecipitationIntensity\":\"Light\"},\"Night\":{\"Icon\":34,\"IconPhrase\":\"Mostly clear\",\"HasPrecipitation\":false},\"Sources\":[\"AccuWeather\"],\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?lang=en-us\"},{\"Date\":\"2021-01-21T07:00:00+02:00\",\"EpochDate\":1611205200,\"Temperature\":{\"Minimum\":{\"Value\":40,\"Unit\":\"F\",\"UnitType\":18},\"Maximum\":{\"Value\":62,\"Unit\":\"F\",\"UnitType\":18}},\"Day\":{\"Icon\":1,\"IconPhrase\":\"Sunny\",\"HasPrecipitation\":false},\"Night\":{\"Icon\":33,\"IconPhrase\":\"Clear\",\"HasPrecipitation\":false},\"Sources\":[\"AccuWeather\"],\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=1&lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=1&lang=en-us\"},{\"Date\":\"2021-01-22T07:00:00+02:00\",\"EpochDate\":1611291600,\"Temperature\":{\"Minimum\":{\"Value\":42,\"Unit\":\"F\",\"UnitType\":18},\"Maximum\":{\"Value\":66,\"Unit\":\"F\",\"UnitType\":18}},\"Day\":{\"Icon\":1,\"IconPhrase\":\"Sunny\",\"HasPrecipitation\":false},\"Night\":{\"Icon\":34,\"IconPhrase\":\"Mostly clear\",\"HasPrecipitation\":false},\"Sources\":[\"AccuWeather\"],\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=2&lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=2&lang=en-us\"},{\"Date\":\"2021-01-23T07:00:00+02:00\",\"EpochDate\":1611378000,\"Temperature\":{\"Minimum\":{\"Value\":42,\"Unit\":\"F\",\"UnitType\":18},\"Maximum\":{\"Value\":67,\"Unit\":\"F\",\"UnitType\":18}},\"Day\":{\"Icon\":1,\"IconPhrase\":\"Sunny\",\"HasPrecipitation\":false},\"Night\":{\"Icon\":34,\"IconPhrase\":\"Mostly clear\",\"HasPrecipitation\":false},\"Sources\":[\"AccuWeather\"],\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=3&lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=3&lang=en-us\"},{\"Date\":\"2021-01-24T07:00:00+02:00\",\"EpochDate\":1611464400,\"Temperature\":{\"Minimum\":{\"Value\":45,\"Unit\":\"F\",\"UnitType\":18},\"Maximum\":{\"Value\":68,\"Unit\":\"F\",\"UnitType\":18}},\"Day\":{\"Icon\":4,\"IconPhrase\":\"Intermittent clouds\",\"HasPrecipitation\":false},\"Night\":{\"Icon\":33,\"IconPhrase\":\"Clear\",\"HasPrecipitation\":false},\"Sources\":[\"AccuWeather\"],\"MobileLink\":\"http://m.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=4&lang=en-us\",\"Link\":\"http://www.accuweather.com/en/il/tel-aviv/215854/daily-weather-forecast/215854?day=4&lang=en-us\"}]}");
            }
        }
    }
}
