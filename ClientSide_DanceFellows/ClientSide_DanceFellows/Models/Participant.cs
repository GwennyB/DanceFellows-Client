using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class Participant
    {
        public int ID { get; set; }
        [Display(Name="WSC ID")]
        public int WSC_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Minimum Level")]
        public Level MinLevel { get; set; }
        [Display(Name = "Maximum Level")]
        public Level MaxLevel { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

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
