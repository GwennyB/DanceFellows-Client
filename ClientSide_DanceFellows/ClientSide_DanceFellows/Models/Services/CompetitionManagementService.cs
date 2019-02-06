using ClientSide_DanceFellows.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientSide_DanceFellows.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientSide_DanceFellows.Models.Services
{
    /// <summary>
    /// Manages CompetitionController functions using dependecy injection.
    /// </summary>
    public class CompetitionManagementService : ICompetitionManager
    {
        //Selects DB
        private ClientSideDanceFellowsDbContext _context { get; }

        /// <summary>
        /// Populates services database.
        /// </summary>
        /// <param name="context"></param>
        public CompetitionManagementService(ClientSideDanceFellowsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new Competition to database.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        public async Task CreateCompetition(Competition competition)
        {
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a competition from the database using Competition as an input.
        /// </summary>
        /// <param name="competition"></param>
        public void DeleteCompetition(Competition competition)
        {
            _context.Competitions.Remove(competition);
            _context.SaveChanges();
        }

        /// <summary>
        /// Performs a search using id as an input then removes competition that matches input id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCompetition(int id)
        {
            Competition competition = _context.Competitions.FirstOrDefault(c => c.ID == id);
            _context.Competitions.Remove(competition);
            _context.SaveChanges();
        }

        /// <summary>
        /// Finds a Competition using Competition.ID as search critera and returns Competition.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Competition</returns>
        public async Task<Competition> GetCompetition(int id)
        {
            return await _context.Competitions.FirstOrDefaultAsync(c => c.ID == id);
        }

        /// <summary>
        /// Collects all Competitions in existence and returns them as a list.
        /// </summary>
        /// <returns>IEnumerable<Competition></returns>
        public async Task<IEnumerable<Competition>> GetCompetitions()
        {
            return await _context.Competitions.ToListAsync();
        }

        /// <summary>
        /// Searches ALL Competitions and returns ALL the CONTAIN compType
        /// </summary>
        /// <param name="compType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Competition>> SearchCompetitions(CompType compType)
        {
            var competitions = from a in _context.Competitions
                               select a;

            competitions = competitions.Where(a => a.CompType == compType);

            return await competitions.ToListAsync();
        }

        /// <summary>
        /// Receives a Competition and updates it in DB
        /// </summary>
        /// <param name="competition"></param>
        public void UpdateCompetition(Competition competition)
        {
            _context.Competitions.Update(competition);
            _context.SaveChanges();
        }

        /// <summary>
        /// Finds all Competitors that have registered for Competition and returns a list of RegisteredCompetitors.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><IEnumerable<RegisteredCompetitor></returns>
        public async Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id)
        {
            var registeredCompetitors = from rc in _context.RegisteredCompetitors
                                        select rc;

            registeredCompetitors = registeredCompetitors.Where(rc => rc.CompetitionID == id);

            return await registeredCompetitors.ToListAsync();
        }
    }
}