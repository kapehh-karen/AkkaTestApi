using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using AkkaTestApi.Actors;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Services
{
    public class WeatherService
    {
        private readonly IActorRef _weatherManager;
        private readonly IdentifierGenerator _identifierGenerator;

        public WeatherService(IActorRefFactory actorSystem, IdentifierGenerator identifierGenerator)
        {
            _weatherManager = actorSystem.ActorOf(WeatherManager.Props());
            _identifierGenerator = identifierGenerator;
        }

        public void CreateCity(string cityName)
        {
            _weatherManager.Tell(new CreateCityMessage(_identifierGenerator.GenerateId(), cityName, null));
        }

        public async Task<CityModel[]> GetCities()
        {
            var respond = await _weatherManager.Ask<RespondAllCitiesMessage>(new RequestAllCitiesMessage());
            return respond.Cities;
        }

        public void UpdateWeather(int id, double weather)
        {
            _weatherManager.Tell(new UpdateWeatherMessage(id, weather));
        }

        public async Task<CityModel> GetWeatherFromCity(int id)
        {
            var respond = await _weatherManager.Ask<RespondWeatherMessage>(new RequestWeatherMessage(id));

            return new CityModel
            {
                Id = respond.Id,
                Name = respond.Name,
                Weather = respond.Weather
            };
        }
    }
}