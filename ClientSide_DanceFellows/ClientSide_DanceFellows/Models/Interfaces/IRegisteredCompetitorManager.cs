using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models.Interfaces
{
    public interface IRegisteredCompetitorManager
    {
        //Create RegisteredCompetitor
        Task CreateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        //Read An RegisteredCompetitor
        Task<RegisteredCompetitor> GetRegisteredCompetitor(int id);

        //Read ALL RegisteredCompetitor
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitor();

        //Update A RegisteredCompetitor
        void UpdateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        //Delete A RegisteredCompetitor
        void DeleteRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        void DeleteRegisteredCompetitor(int id);

        //Search RegisteredCompetitor
        Task<IEnumerable<RegisteredCompetitor>> SearchRegisteredCompetitor(int bibNumber);

        //Navagation Properties

        //Adds RegisteredCompetitor to Participant nav props
        Task AddParticipantAssociation(RegisteredCompetitor registeredCompetitor);

        //Adds RegisteredCompetitor to Competition nav props
        Task AddCompetitionAssociation(RegisteredCompetitor registeredCompetitor);

        //TODO: Add submit to API Task
    }
}
