using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class UserDTO
    {
        // DTO står for Data Transfer Object. Det bliver brugt når man vil overføre en entitet, men uden en eller flere properties.
        public UserDTO()
        {

        }
        public int UserID { get; set; }
        public string Username { get; set; }
        //public string Email { get; set; }
    }
}
