using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Actors
{
    public class WeatherManager : ReceiveActor
    {
        private Dictionary<string, IActorRef> _cityActors = new Dictionary<string, IActorRef>();

        public WeatherManager()
        {
            Receive<CreateCityMessage>(message =>
            {
                if (_cityActors.ContainsKey(message.Name))
                {
                    return;
                }

                var weatherActor = Context.ActorOf(WeatherCity.Props(message.Name, null));
                _cityActors[message.Name] = weatherActor;
            });

            Receive<UpdateWeatherMessage>(message => _cityActors[message.Name].Forward(message));

            Receive<RequestWeatherMessage>(message => _cityActors[message.Name].Forward(message));

            ReceiveAsync<RequestAllCitiesMessage>(async message =>
            {
                var list = new List<CityModel>();

                foreach (var (cityName, actorRef) in _cityActors)
                {
                    var respond = await actorRef.Ask<RespondWeatherMessage>(new RequestWeatherMessage(null));
                    var cityModel = new CityModel
                    {
                        Name = cityName,
                        Weather = respond.Weather
                    };
                    list.Add(cityModel);
                }

                Sender.Tell(new RespondAllCitiesMessage(list.ToArray()));
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new WeatherManager());
        }
    }
}