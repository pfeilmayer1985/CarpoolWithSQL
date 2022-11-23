using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    /// <summary>
    /// Main class for users (drivers and passengers)
    /// </summary>
    public abstract class UsersC
    {
        /// <summary>
        /// user class properties
        /// </summary>

        public string ID { get; set; }
        public string Name { get; set; }
        public string StartingCity { get; set; }
        public string Destination { get; set; }
        public List<UsersC> AllUserList { get; set; }

        /// <summary>
        /// user class constructor
        /// </summary>
        public UsersC()
        {
            AllUserList = new List<UsersC>();

        }
    }
}
