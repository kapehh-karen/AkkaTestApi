using System;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Services;

namespace AkkaTestApi.Actors
{
    public class CityWeatherActor : ReceiveActor
    {
        private double _weather;

        public CityWeatherActor()
        {
            Receive<UpdateWeatherMessage>(updateWeatherMessage => _weather = updateWeatherMessage.Weather);
            Receive<RequestWeatherMessage>(_ => Sender.Tell(new RespondWeatherMessage(_weather)));
        }
    }
}