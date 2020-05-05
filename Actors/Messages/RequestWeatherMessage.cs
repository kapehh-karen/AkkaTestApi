namespace AkkaTestApi.Actors.Messages
{
    public class RequestWeatherMessage
    {
        public RequestWeatherMessage()
        {
        }

        public RequestWeatherMessage(int id)
        {
            Id = id;
        }

        public int? Id { get; }
    }
}