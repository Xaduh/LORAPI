using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class FriendsList
    {        
        public int UserFriendID { get; set; }
        public int UserID { get; set; }
        public int FriendID { get; set; }
        public int StatusID { get; set; }
    }
}
