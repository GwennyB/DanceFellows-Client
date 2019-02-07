using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        Task<RegisteredCompetitor> GetRegisteredCompetitor(int participantID, int competitionID);

        //Read ALL RegisteredCompetitor
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors();

        //Update A RegisteredCompetitor
        void UpdateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        //Delete A RegisteredCompetitor
        void DeleteRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        void DeleteRegisteredCompetitor(int participantID, int competitionID);

        //Search RegisteredCompetitor
        Task<IEnumerable<RegisteredCompetitor>> SearchRegisteredCompetitor(string searchString);

        //Navagation Properties

        Task<Participant> ShowParticipant(int participantID);

        Task<Competition> ShowCompetition(int participantID);

        //Searches through participants and only lists ones that have WSC ID
        Task<IEnumerable<Participant>> ListValidCompetitors();

        //Show Existing Competitions
        Task<IEnumerable<Competition>> ListCompetitions();

        //TODO: Add submit to API Task
    }
}
