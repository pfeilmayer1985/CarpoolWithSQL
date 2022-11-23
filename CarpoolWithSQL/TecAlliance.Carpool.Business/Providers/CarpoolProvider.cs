using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class CarpoolModelProvider : IExamplesProvider<CarpoolsModelDto>
    {
        public CarpoolsModelDto GetExamples()
        {
            return new CarpoolsModelDto()
            {
                Driver = { },
                FreeSeatsRemaining = 1,
                Origin = "Schrozberg",
                Destination = "Weikersheim",
                DepartureDate = new DateTime(2022,12,31,13,30,45)
                
            };
        }
    }
}

