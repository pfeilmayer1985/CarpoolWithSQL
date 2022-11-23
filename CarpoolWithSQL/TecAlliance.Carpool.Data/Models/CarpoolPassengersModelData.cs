using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    /// <summary>
    /// Main class for passengers
    /// </summary>
    public class CarpoolPassengersModelData
    {
        /// <summary>
        /// user class properties
        /// </summary>
        public int Carpool_ID { get; set; }
        public int User_ID { get; set; }

        /// <summary>
        /// user class constructor
        /// </summary>
        public CarpoolPassengersModelData(int carpoolID, int userID)
        {
            Carpool_ID= carpoolID;
            User_ID= userID;
        }

        public CarpoolPassengersModelData()
        {

        }
    }
}
