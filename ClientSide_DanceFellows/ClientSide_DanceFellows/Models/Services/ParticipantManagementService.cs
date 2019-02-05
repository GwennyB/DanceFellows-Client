using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientSide_DanceFellows.Data;
using ClientSide_DanceFellows.Models.Interfaces;

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

        public async Task<IEnumerable<Participant>> GetCParticipants()
        {
            throw new NotImplementedException();
        }

        public async Task<Participant> GetParticipant(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Competition>> SearchParticipants(string lastName)
        {
            throw new NotImplementedException();
        }

        public void UpdateParticipant(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}
