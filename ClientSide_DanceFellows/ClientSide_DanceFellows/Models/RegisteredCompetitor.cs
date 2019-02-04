using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class RegisteredCompetitor
    {
        public int ParticipantID { get; set; }
        public int CompetitionID { get; set; }
        public Role Role { get; set; }
        public Placement Placement { get; set; }
        public int BibNumber { get; set; }
        public int ChiefJudgeScore { get; set; }
        public int JudgeOneScore { get; set; }
        public int JudgeTwoScore { get; set; }
        public int JudgeThreeScore { get; set; }
        public int JudgeFourScore { get; set; }
        public int JudgeFiveScore { get; set; }
        public int JudgeSixScore { get; set; }

        //navigation properties
        public Participant Participant { get; set; }
        public Competition Competition { get; set; }
    }

    public enum Role
    {
        Lead=0,
        Follow=1
    }

    public enum Placement
    {
        Finalled=0,
        Position5=1,
        Position4=2,
        Position3=3,
        Position2=4,
        Position1=5
    }
}
