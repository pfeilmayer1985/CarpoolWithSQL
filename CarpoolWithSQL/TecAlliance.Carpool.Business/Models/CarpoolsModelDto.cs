﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpoolsModelDto
    {
        public int? CarpoolID { get; set;  }
        public UserBaseModelDto Driver { get; set; }
        public int TotalSeatsCount { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        
        public CarpoolsModelDto(int carpoolID, UserBaseModelDto driver, int totalSeatsCount, string origin, string destination, DateTime departureDate)
        {
            CarpoolID = carpoolID;
            Driver = driver;
            TotalSeatsCount = totalSeatsCount;
            Origin = origin;
            Destination = destination;
            DepartureDate = departureDate;
        
        }

        public CarpoolsModelDto()
        {

        }
    }
}
