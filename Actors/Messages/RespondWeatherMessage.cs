namespace AkkaTestApi.Actors.Messages
{
    public class RespondWeatherMessage
    {
        public RespondWeatherMessage(int id, string name, double? weather)
        {
            Id = id;
            Name = name;
            Weather = weather;
        }

        public int Id { get; }

        public string Name { get; }

        public double? Weather { get; }
    }
}