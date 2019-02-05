using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models.Interfaces
{
    public interface ICompetitionManager
    {
        //Create Competition
        Task CreateCompetition(Competition competition);

        //Read An Competition
        Task GetCompetition(int id);

        Task<Competition> GetCompetition(string name);

        //Read ALL Amenities
        Task<IEnumerable<Competition>> GetCompetitions();

        //Update A Competition
        void UpdateCompetition(Competition competition);

        //Delete A Competition
        void DeleteCompetition(Competition competition);

        //Search Competitions
        Task<IEnumerable<Competition>> SearchCompetitions(string searchString);

    }
}
