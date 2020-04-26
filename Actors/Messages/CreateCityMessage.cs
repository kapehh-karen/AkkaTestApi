namespace AkkaTestApi.Actors.Messages
{
    public class CreateCityMessage
    {
        public CreateCityMessage(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}