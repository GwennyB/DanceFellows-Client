using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientSide_DanceFellows.Data;
using ClientSide_DanceFellows.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClientSide_DanceFellows.Models.Services
{
    public class RegisteredCompetitorManagementService : IRegisteredCompetitorManager
    {
        //Selects DB
        private ClientSideDanceFellowsDbContext _context { get; }

        /// <summary>
        /// Populates services database.
        /// </summary>
        /// <param name="context"></param>
        public RegisteredCompetitorManagementService(ClientSideDanceFellowsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new RegisteredCompetitor to db.
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        public async Task CreateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor)
        {
            Participant participant = await _context.Participants.FirstOrDefaultAsync(p => p.ID == registeredCompetitor.ParticipantID);
            registeredCompetitor.Participant = participant;

            Competition competition = await _context.Competitions.FirstOrDefaultAsync(p => p.ID == registeredCompetitor.CompetitionID);
            registeredCompetitor.Participant = participant;
            registeredCompetitor.Competition = competition;

            _context.RegisteredCompetitors.Add(registeredCompetitor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a RegisteredCompetitor from the database using registeredCompetitor as an input.
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        public void DeleteRegisteredCompetitor(RegisteredCompetitor registeredCompetitor)
        {
            _context.RegisteredCompetitors.Remove(registeredCompetitor);
            _context.SaveChanges();
        }

        /// <summary>
        /// Searches for RegisteredCompetitor using composite key info and removes from DB.
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="competitionID"></param>
        public void DeleteRegisteredCompetitor(int participantID, int competitionID)
        {
            RegisteredCompetitor registeredCompetitor = _context.RegisteredCompetitors.FirstOrDefault(rc => rc.CompetitionID == competitionID && rc.ParticipantID == participantID);

            _context.RegisteredCompetitors.Remove(registeredCompetitor);
            _context.SaveChanges();
        }

        /// <summary>
        /// Searches for RegisteredCompetitor using composite key info returns RegisteredCompetitor
        /// </summary>
        public async Task<RegisteredCompetitor> GetRegisteredCompetitor(int participantID, int competitionID)
        {
            return await _context.RegisteredCompetitors.FirstOrDefaultAsync(rc => rc.CompetitionID == competitionID && rc.ParticipantID == participantID);
        }

        public async Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors()
        {
            return await _context.RegisteredCompetitors.ToListAsync();
        }

        /// <summary>
        /// Searches for RegisteredCompetitors corresponding to a specific competition using competition ID and returns a list.
        /// </summary>
        /// <param name="competitionID"></param>
        /// <returns>List of RegisteredCompetitors</returns>
        public async Task<IEnumerable<RegisteredCompetitor>> SearchRegisteredCompetitor(string searchString)
        {
            var competitions = from c in _context.Competitions
                               select c;

            competitions = competitions.Where(rc => rc.CompetitionName.ToLower().Contains(searchString.ToLower()));

            var registeredCompetitors = from rc in _context.RegisteredCompetitors
                                        select rc;
            List<RegisteredCompetitor> validRegisteredCompetitors = new List<RegisteredCompetitor>();
            foreach(Competition competition in competitions)
            {
                foreach(RegisteredCompetitor registeredCompetitor in registeredCompetitors)
                {
                    if (competition.ID == registeredCompetitor.CompetitionID)
                    {
                        validRegisteredCompetitors.Add(registeredCompetitor);
                    }
                }
                
            }

            return validRegisteredCompetitors;
        }

        /// <summary>
        /// Updates and existing RegisteredCompetitor
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        public void UpdateRegisteredCompetitor(RegisteredCompetitor registeredCompetitor)
        {
            _context.RegisteredCompetitors.Update(registeredCompetitor);
            _context.SaveChanges();
        }

        // Nav Props     

        public async Task<IEnumerable<Participant>> ListValidCompetitors()
        {
            var validCompetitors = _context.Participants.Where(vc => vc.EligibleCompetitor == true);
            return validCompetitors;
        }

        public async Task<IEnumerable<Competition>> ListCompetitions()
        {
            var competitions = _context.Competitions;
            return competitions;
        }

        public async Task<Participant> ShowParticipant(int participantID)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(p => p.ID == participantID);
            return participant;
        }

        public async Task<Competition> ShowCompetition(int participantID)
        {
            var competition = await _context.Competitions.FirstOrDefaultAsync(p => p.ID == participantID);
            return competition;
        }
    }
}
