using Akka.Actor;
using AkkaTestApi.Actors.Messages;

namespace AkkaTestApi.Actors
{
    public class WeatherCity : ReceiveActor
    {
        public WeatherCity(int id, string name, double? weather)
        {
            Receive<UpdateWeatherMessage>(message => weather = message.Weather);
            Receive<RequestWeatherMessage>(_ => Sender.Tell(new RespondWeatherMessage(id, name, weather)));
        }

        public static Props Props(int id, string name, double? weather)
        {
            return Akka.Actor.Props.Create(() => new WeatherCity(id, name, weather));
        }
    }
}