using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaTestApi.Actors;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Services
{
    public class WeatherService
    {
        private readonly IActorRef _weatherManager;

        public WeatherService(ActorSystem actorSystem)
        {
            _weatherManager = actorSystem.ActorOf(Props.Create<WeatherManager>(true));
        }

        public void CreateCity(string cityName)
        {
            _weatherManager.Tell(new CreateCityMessage(cityName));
        }

        public async Task<CityModel[]> GetCities()
        {
            var respond = await _weatherManager.Ask<RespondAllCitiesMessage>(new RequestAllCitiesMessage());
            return respond.Cities;
        }

        public void UpdateWeather(string cityName, double weather)
        {
            _weatherManager.Tell(new UpdateWeatherMessage(cityName, weather));
        }

        public async Task<double?> GetWeatherFromCity(string cityName)
        {
            var respond = await _weatherManager.Ask<RespondWeatherMessage>(new RequestWeatherMessage(cityName));
            return respond.Weather;
        }
    }
}