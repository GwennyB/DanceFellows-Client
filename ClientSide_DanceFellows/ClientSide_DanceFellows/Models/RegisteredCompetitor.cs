using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class RegisteredCompetitor
    {
        [Required(ErrorMessage = "")]
        [Display(Name = "Participant ID")]
        public int ParticipantID { get; set; }

        [Required(ErrorMessage = "")]
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }

        public int EventID { get; set; }

        [Required(ErrorMessage = "")]
        public Role Role { get; set; }

        public Placement Placement { get; set; }

        [Required(ErrorMessage = "Please provide a bib number")]
        [Display(Name = "Bib Number")]
        public int BibNumber { get; set; }

        [Display(Name = "Chief Judge Score")]
        public int ChiefJudgeScore { get; set; }

        [Display(Name = "Judge One Score")]
        public int JudgeOneScore { get; set; }

        [Display(Name = "Judge Two Score")]
        public int JudgeTwoScore { get; set; }

        [Display(Name = "Judge Three Score")]
        public int JudgeThreeScore { get; set; }

        [Display(Name = "Judge Four Score")]
        public int JudgeFourScore { get; set; }

        [Display(Name = "Judge Five Score")]
        public int JudgeFiveScore { get; set; }

        [Display(Name = "Judge Six Score")]
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
        [Display(Name = "Position 5")]
        Position5 =1,
        [Display(Name = "Position 4")]
        Position4 =2,
        [Display(Name = "Position 3")]
        Position3 =3,
        [Display(Name = "Position 4")]
        Position2 =4,
        [Display(Name = "Position 5")]
        Position1 =5
    }
}
