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
        /// <summary>
        /// Create RegisteredCompetitor
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        Task CreateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        /// <summary>
        /// Read An RegisteredCompetitor
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="competitionID"></param>
        /// <returns></returns>
        Task<RegisteredCompetitor> GetRegisteredCompetitor(int participantID, int competitionID);

        /// <summary>
        /// Read ALL RegisteredCompetitor
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors();

        /// <summary>
        /// Update A RegisteredCompetitor
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        Task UpdateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        /// <summary>
        /// Update A RegisteredCompetitor
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        Task DeleteRegisteredCompetitor(RegisteredCompetitor registeredCompetitor);

        /// <summary>
        /// Search RegisteredCompetitor
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<IEnumerable<RegisteredCompetitor>> SearchRegisteredCompetitor(string searchString);

        //Navagation Properties

        /// <summary>
        /// Shows nav prop Participant
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        Task<Participant> ShowParticipant(int participantID);

        /// <summary>
        /// Shows nav prop Competition
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        Task<Competition> ShowCompetition(int participantID);

        /// <summary>
        /// Searches through participants and only lists ones that have WSC ID
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Participant>> ListValidCompetitors();

        /// <summary>
        /// Show Existing Competitions
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Competition>> ListCompetitions();

    }
}
