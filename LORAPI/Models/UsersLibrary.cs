using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class UsersLibrary
    {
        public int UserGameID { get; set; }
        public int GameID { get; set; }
        public int UserID { get; set; }
        public DateTime Acquired { get; set; }
    }
}
