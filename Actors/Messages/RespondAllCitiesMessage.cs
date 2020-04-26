using AkkaTestApi.Models;

namespace AkkaTestApi.Actors.Messages
{
    public class RespondAllCitiesMessage
    {
        public RespondAllCitiesMessage(CityModel[] cities)
        {
            Cities = cities;
        }

        public CityModel[] Cities { get; }
    }
}