using System.Threading.Tasks;
using AkkaTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AkkaTestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost("{cityName}")]
        public IActionResult Create(string cityName)
        {
            _weatherService.CreateCity(cityName);
            return Ok("Ok");
        }

        [HttpPost("{cityName}")]
        public IActionResult UpdateWeather(string cityName, [FromForm] double weather)
        {
            _weatherService.UpdateWeather(cityName, weather);
            return Ok("Ok");
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _weatherService.GetCities();
            return Ok(cities);
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            return Ok(new
            {
                City = cityName,
                Weather = await _weatherService.GetWeatherFromCity(cityName)
            });
        }
    }
}