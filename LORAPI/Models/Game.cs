using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class Game
    {
        // Det kunne være en god idé at lave en GameDTO, angående Path. Da vi ikke bruger denne til mere end download og bibliotek, så bliver det ikke gjort.
        // DTO modeller bliver oprettet når der er data vi ikke vil have en normal bruger til at kunne tilgå.
        public int GameID { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}
