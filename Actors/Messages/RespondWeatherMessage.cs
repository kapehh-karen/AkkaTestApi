namespace AkkaTestApi.Actors.Messages
{
    public class RespondWeatherMessage
    {
        public RespondWeatherMessage(string name, double? weather)
        {
            Name = name;
            Weather = weather;
        }

        public string Name { get; }
        public double? Weather { get; }
    }
}