namespace AkkaTestApi.Actors.Messages
{
    public class CreateCityMessage
    {
        public CreateCityMessage(int id, string name, double? weather)
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