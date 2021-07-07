using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int id)
        {
            UserID = id;
        }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
