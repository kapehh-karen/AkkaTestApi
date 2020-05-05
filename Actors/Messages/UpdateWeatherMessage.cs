namespace AkkaTestApi.Actors.Messages
{
    public class UpdateWeatherMessage
    {
        public UpdateWeatherMessage(int id, double weather)
        {
            Id = id;
            Weather = weather;
        }

        public int Id { get; }

        public double Weather { get; }
    }
}