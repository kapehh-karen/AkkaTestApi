namespace AkkaTestApi.Actors.Messages
{
    public class RequestWeatherMessage
    {
        public RequestWeatherMessage(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}