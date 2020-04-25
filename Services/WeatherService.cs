using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaTestApi.Actors;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Services
{
    public class WeatherService
    {
        private readonly ActorSystem _actorSystem;

        private readonly ConcurrentDictionary<string, IActorRef>
            _cities = new ConcurrentDictionary<string, IActorRef>();

        public WeatherService(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        public void CreateCity(string cityName)
        {
            if (cityName != null && !_cities.ContainsKey(cityName))
            {
                _cities[cityName] = _actorSystem.ActorOf(Props.Create<CityWeatherActor>());
            }
        }

        public async Task<CityModel[]> GetCities()
        {
            var modelTasks = _cities
                .Select(async x =>
                {
                    var (cityName, actionRef) = x;
                    var respond = await actionRef.Ask<RespondWeatherMessage>(new RequestWeatherMessage());

                    return new CityModel
                    {
                        Name = cityName,
                        Weather = respond.Weather
                    };
                })
                .ToArray();

            return await Task.WhenAll(modelTasks);
        }

        public void UpdateWeather(string cityName, double weather)
        {
            if (cityName != null && _cities.TryGetValue(cityName, out var actor))
            {
                actor.Tell(new UpdateWeatherMessage(weather));
            }
        }

        public async Task<double?> GetWeatherFromCity(string cityName)
        {
            if (cityName == null || !_cities.TryGetValue(cityName, out var actorRef))
            {
                return null;
            }

            var respond = await actorRef.Ask<RespondWeatherMessage>(new RequestWeatherMessage());
            return respond.Weather;
        }
    }
}