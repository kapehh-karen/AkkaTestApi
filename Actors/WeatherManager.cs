using System.Collections.Generic;
using Akka.Actor;
using AkkaTestApi.Actors.Messages;
using AkkaTestApi.Models;

namespace AkkaTestApi.Actors
{
    public class WeatherManager : ReceiveActor
    {
        public WeatherManager()
        {
            Receive<CreateCityMessage>(message =>
            {
                var existsActor = Context.Child(message.Id.ToString());
                if (existsActor.IsNobody())
                {
                    Context.ActorOf(
                        WeatherCity.Props(message.Id, message.Name, message.Weather),
                        message.Id.ToString());
                }
            });

            Receive<UpdateWeatherMessage>(message =>
            {
                var existsActor = Context.Child(message.Id.ToString());
                if (!existsActor.IsNobody())
                {
                    existsActor.Forward(message);
                }
            });

            Receive<RequestWeatherMessage>(message =>
            {
                var existsActor = Context.Child(message.Id.ToString());
                if (!existsActor.IsNobody())
                {
                    existsActor.Forward(message);
                }
            });

            ReceiveAsync<RequestAllCitiesMessage>(async message =>
            {
                var list = new List<CityModel>();

                foreach (var actorRef in Context.GetChildren())
                {
                    var respond = await actorRef.Ask<RespondWeatherMessage>(new RequestWeatherMessage());
                    var cityModel = new CityModel
                    {
                        Id = respond.Id,
                        Name = respond.Name,
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