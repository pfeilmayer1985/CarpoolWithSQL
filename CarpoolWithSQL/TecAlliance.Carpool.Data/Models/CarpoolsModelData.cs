using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpoolsModelData
    {
        public int? CarpoolID { get; set; }
        public int DriverID { get; set; }
        public int TotalSeatsCount { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        

        public CarpoolsModelData(int carpoolID, int driverID, int totalSeatsCount, string origin, string destination, DateTime departureDate)
        {
            CarpoolID = carpoolID;
            DriverID = driverID;
            TotalSeatsCount = totalSeatsCount;
            Origin = origin;
            Destination = destination;
            DepartureDate = departureDate;
        }
        public CarpoolsModelData()
        {

        }
    }
}
