namespace AkkaTestApi.Actors.Messages
{
    public class UpdateWeatherMessage
    {
        public UpdateWeatherMessage(string name, double weather)
        {
            Name = name;
            Weather = weather;
        }

        public string Name { get; }
        public double Weather { get; }

        public override string ToString()
        {
            return $"UpdateWeatherMessage(Name={Name},Weather={Weather})";
        }
    }
}