using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;

namespace AkkaTestApi.Actors
{
    public class WeatherManager : ReceiveActor
    {
        private Dictionary<string, IActorRef> _cityActors = new Dictionary<string, IActorRef>();

        private double _weather;

        public WeatherManager(bool root)
        {
            Receive<CreateCityMessage>(message =>
            {
                Console.WriteLine($"Receive<CreateCityMessage> (root={root}) {this}, message {message}");
                if (root)
                {
                    if (_cityActors.ContainsKey(message.Name))
                    {
                        return;
                    }

                    var weatherActor = Context.ActorOf(Props.Create<WeatherManager>(false));
                    Context.Watch(weatherActor);
                    _cityActors[message.Name] = weatherActor;
                }
            });

            Receive<UpdateWeatherMessage>(message =>
            {
                Console.WriteLine($"Receive<UpdateWeatherMessage> (root={root}) {this}, message {message}");
                if (root)
                {
                    _cityActors[message.Name].Forward(message);
                }
                else
                {
                    _weather = message.Weather;
                }
            });

            Receive<RequestWeatherMessage>(message =>
            {
                Console.WriteLine($"Receive<RequestWeatherMessage> (root={root}) {this}, message {message}");
                if (root)
                {
                    _cityActors[message.Name].Forward(message);
                }
                else
                {
                    Sender.Tell(new RespondWeatherMessage(_weather));
                }
            });

            Receive<RequestAllCitiesMessage>(message =>
            {
                if (root)
                {
                    var weatherAllCities =
                        Context.ActorOf(WeatherAllCities.Props(
                            _cityActors
                                .Select(x => (x.Key, x.Value))
                                .ToArray()));
                    weatherAllCities.Forward(message);
                }
            });
        }

        public override string ToString()
        {
            return $"WeatherManager(_weather={_weather})";
        }
    }
}