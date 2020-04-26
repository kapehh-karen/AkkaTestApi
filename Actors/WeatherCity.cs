using Akka.Actor;
using AkkaTestApi.Actors.Messages;

namespace AkkaTestApi.Actors
{
    public class WeatherCity : ReceiveActor
    {
        public WeatherCity(string name, double? weather)
        {
            Receive<UpdateWeatherMessage>(message => weather = message.Weather);
            Receive<RequestWeatherMessage>(_ => Sender.Tell(new RespondWeatherMessage(name, weather)));
        }
        
        public static Props Props(string name, double? weather)
        {
            return Akka.Actor.Props.Create(() => new WeatherCity(name, weather));
        }
    }
}