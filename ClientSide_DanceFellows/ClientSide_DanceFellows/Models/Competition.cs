using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class Competition
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please select a competition type")]
        [Display(Name = "Competition Type")]
        public CompType CompType { get; set; }

        [Required(ErrorMessage = "Please select a competition level")]
        public Level Level { get; set; }

        //navigation properties
        [JsonIgnore]
        public ICollection<RegisteredCompetitor> RegisteredCompetitors { get; set; }
    }

    public enum CompType
    {
        [Display(Name = "Jack and Jill")]
        JackAndJill =0,
        Strictly=1,
        Classic=2,
        Showcase=3,
        [Display(Name = "Rising Star")]
        RisingStar =4
    }
}
