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
        Task<Competition> GetCompetition(int id);

        //Read ALL Amenities
        Task<IEnumerable<Competition>> GetCompetitions();

        //Update A Competition
        void UpdateCompetition(Competition competition);

        //Delete A Competition
        void DeleteCompetition(Competition competition);

        void DeleteCompetition(int id);

        //Search Competitions
        Task<IEnumerable<Competition>> SearchCompetitions(CompType compType);

        //Navagation Properties

        //Add NavProp to RegisteredCompetitors
        Task AddCompetitionAssociation(Competition competition);

        //Remove NavProp from RegisteredCompetitors
        Task RemoveCompetitionAssociation(Competition competition);


        //Read ALL RegisteredCompetitiors
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id);

    }
}
