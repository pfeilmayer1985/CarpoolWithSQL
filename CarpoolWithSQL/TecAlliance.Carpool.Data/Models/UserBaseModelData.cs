using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    /// <summary>
    /// Main class for users
    /// </summary>
    public class UserBaseModelData
    {
        /// <summary>
        /// user class properties
        /// </summary>
        public int? ID { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDriver { get; set; }

        /// <summary>
        /// user class constructor
        /// </summary>
        public UserBaseModelData(int id, string email, string phoneNo, string password, string firstName, string lastName, bool isDriver)
        {
            ID = id;
            Email = email;
            PhoneNo = phoneNo;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            IsDriver = isDriver;

        }

        public UserBaseModelData()
        {
        }
    }
}
