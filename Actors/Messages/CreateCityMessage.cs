namespace AkkaTestApi.Actors.Messages
{
    public class CreateCityMessage
    {
        public CreateCityMessage(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"CreateCityMessage(Name={Name})";
        }
    }
}