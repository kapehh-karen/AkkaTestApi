using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AkkaTestApi.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Create([Required] string cityName)
        {
            if (!ModelState.IsValid) return BadRequest();

            _weatherService.CreateCity(cityName);
            return Ok("Ok");
        }

        public IActionResult UpdateWeather([Required] string cityName, [Required] double weather)
        {
            if (!ModelState.IsValid) return BadRequest();

            _weatherService.UpdateWeather(cityName, weather);
            return Ok("Ok");
        }

        public async Task<IActionResult> GetCities()
        {
            var cities = await _weatherService.GetCities();
            return Ok(cities);
        }

        public async Task<IActionResult> GetWeather([Required] string cityName)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(new
            {
                City = cityName,
                Weather = await _weatherService.GetWeatherFromCity(cityName)
            });
        }
    }
}