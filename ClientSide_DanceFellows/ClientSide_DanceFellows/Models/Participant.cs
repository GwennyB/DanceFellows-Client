using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public int WSC_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Level MinLevel { get; set; }
        public Level MaxLevel { get; set; }

        //navigation properties
        public ICollection<RegisteredCompetitor> RegisteredCompetitors { get; set; }

        public ICollection<Participant> EligibleCompetitors { get; set; }

    }

    public enum Level
    {
        Newcomer=0,
        Novice=1,
        Intermediate=2,
        Advanced=3,
        AllStar=4,
        Champ=5
    }
}
