using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientSide_DanceFellows.Data;
using ClientSide_DanceFellows.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientSide_DanceFellows.Models.Services
{
    public class ParticipantManagementService : IParticipantManager
    {
        //Selects DB
        private ClientSideDanceFellowsDbContext _context { get; }

        /// <summary>
        /// Populates services database.
        /// </summary>
        /// <param name="context"></param>
        public ParticipantManagementService(ClientSideDanceFellowsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new Participant to database.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        public async Task CreateParticipant(Participant participant)
        {
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            if (participant.WSC_ID != 0)
            {
                participant.EligibleCompetitor = true;
                _context.Update(participant);
            }
            
            await _context.SaveChangesAsync();
        }
        
        /// <summary>
        /// Deletes Participant from database.
        /// </summary>
        /// <param name="participant"></param>
        public void DeleteParticipant(Participant participant)
        {
            _context.Participants.Remove(participant);
            _context.SaveChanges();
        }

        /// <summary>
        /// Collects all Participants and returns them as a list.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Participant>> GetParticipants()
        {
            return await _context.Participants.ToListAsync();
        }

        /// <summary>
        /// Finds a Participant using Participant.ID as search critera and returns Participant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Participant> GetParticipant(int id)
        {
            return await _context.Participants.FirstOrDefaultAsync(p => p.ID == id);
        }

        /// <summary>
        /// Finds all Competitions that a participant has registered for and returns a list of RegisteredCompetitors.
        /// </summary>
        public async Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id)
        {
            var registeredCompetitors = from rc in _context.RegisteredCompetitors
                                        select rc;

            registeredCompetitors = registeredCompetitors.Where(a => a.ParticipantID == id);

            return await registeredCompetitors.ToListAsync();
        }   
    }
}
