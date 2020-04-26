using System.Collections.Generic;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Actors
{
    public class WeatherAllCities : ReceiveActor
    {
        public WeatherAllCities((string, IActorRef)[] items)
        {
            Receive<RequestAllCitiesMessage>(async message =>
            {
                var sender = Sender;
                var list = new List<CityModel>();

                foreach (var (cityName, actorRef) in items)
                {
                    var respond = await actorRef.Ask<RespondWeatherMessage>(new RequestWeatherMessage(null));
                    var cityModel = new CityModel
                    {
                        Name = cityName,
                        Weather = respond.Weather
                    };
                    list.Add(cityModel);
                }

                sender.Tell(new RespondAllCitiesMessage(list.ToArray()));
            });
        }

        public static Props Props((string, IActorRef)[] items)
        {
            return Akka.Actor.Props.Create(() => new WeatherAllCities(items));
        }
    }
}