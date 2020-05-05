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

        /*
            fetch("https://localhost:5001/weather/create/Test", {
              method: 'POST'
            });
         */
        [HttpPost("{cityName}")]
        public IActionResult Create(string cityName)
        {
            _weatherService.CreateCity(cityName);
            return Ok();
        }

        /*
            fetch("https://localhost:5001/weather/updateWeather/1", {
              method: 'POST',
              headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
              body: "weather=11"
            });
         */
        [HttpPost("{id:int}")]
        public IActionResult UpdateWeather(int id, [FromForm] double weather)
        {
            _weatherService.UpdateWeather(id, weather);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _weatherService.GetCities());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetWeather(int id)
        {
            return Ok(await _weatherService.GetWeatherFromCity(id));
        }
    }
}