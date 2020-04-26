namespace AkkaTestApi.Actors.Messages
{
    public class RespondWeatherMessage
    {
        public RespondWeatherMessage(double weather)
        {
            Weather = weather;
        }

        public double Weather { get; }

        public override string ToString()
        {
            return $"RespondWeatherMessage(Weather={Weather})";
        }
    }
}