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
        }

        // TODO: Add CreateParticipant that searches backend DB.

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
        /// Performs a search using id as an input then removes participant that matches input id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParticipant(int id)
        {
            Participant participant = _context.Participants.FirstOrDefault(p => p.ID == id);
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

        /// <summary>
        /// Searches existing participants and returns participant that matches input lastName.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns>Participant</returns>
        public async Task<IEnumerable<Participant>> SearchParticipants(string lastName)
        {
            var participant = from p in _context.Participants
                               select p;

            participant = participant.Where(p => p.LastName == lastName);

            return await participant.ToListAsync();
        }

        /// <summary>
        /// Receives a Participant and updates it in the DB
        /// </summary>
        /// <param name="participant"></param>
        public void UpdateParticipant(Participant participant)
        {
            _context.Participants.Update(participant);
            _context.SaveChanges();
        }

        public async Task AddParticipantAssociation(Participant participant)
        {
            RegisteredCompetitor registeredCompetitor = _context.RegisteredCompetitors.FirstOrDefault(rc => rc.ParticipantID == participant.ID);

            registeredCompetitor.Participant = participant;

            _context.RegisteredCompetitors.Update(registeredCompetitor);
            _context.SaveChanges();
        }

        public async Task RemoveParticipantAssociation(Participant participant)
        {
            RegisteredCompetitor registeredCompetitor = _context.RegisteredCompetitors.FirstOrDefault(rc => rc.ParticipantID == participant.ID);

            registeredCompetitor.Competition = null;

            _context.RegisteredCompetitors.Update(registeredCompetitor);
            _context.SaveChanges();
        }

        
    }
}
