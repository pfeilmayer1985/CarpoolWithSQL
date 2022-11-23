using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    /// <summary>
    /// Main class for users (drivers and passengers)
    /// </summary>
    public class UserBaseModelDto
    {
        public UserBaseModelDto(int? iD, string email, string phoneNo, string firstName, string lastName, bool isDriver)
        {
            ID = iD;
            Email = email;
            PhoneNo = phoneNo;
            FirstName = firstName;
            LastName = lastName;
            IsDriver = isDriver;
        }

        /// <summary>
        /// user class properties
        /// </summary>
        public int? ID { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDriver { get; set; }


       
    }
}
