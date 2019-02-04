using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class Competition
    {
        public int ID { get; set; }
        public CompType CompType { get; set; }
        public Level Level { get; set; }

        //navigation properties
        public RegisteredCompetitor RegisteredCompetitor { get; set; }
    }

    public enum CompType
    {
        JackAndJill=0,
        Strictly=1,
        Classic=2,
        Showcase=3,
        RisingStar=4
    }
}
