using System;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Services;

namespace AkkaTestApi.Actors
{
    public class CityWeatherActor : UntypedActor
    {
        private double _weather;

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case UpdateWeatherMessage updateWeatherMessage:
                    _weather = updateWeatherMessage.Weather;
                    break;

                case RequestWeatherMessage _:
                    Sender.Tell(new RespondWeatherMessage(_weather));
                    break;
            }
        }
    }
}