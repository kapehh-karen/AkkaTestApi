namespace AkkaTestApi.Actors.Messages
{
    public class UpdateWeatherMessage
    {
        public UpdateWeatherMessage(double weather)
        {
            Weather = weather;
        }

        public double Weather { get; }
    }
}