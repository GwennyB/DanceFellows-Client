﻿using System;
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
        public async Task<IEnumerable<RegisteredCompetitor>> SearchRegisteredCompetitor(int competitionID)
        {
            var registeredCompetitor = from rc in _context.RegisteredCompetitors
                               select rc;

            registeredCompetitor = registeredCompetitor.Where(rc => rc.CompetitionID == competitionID);

            return await registeredCompetitor.ToListAsync();
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

        /// <summary>
        /// Adds RegisteredCompetitor to Competition Nav Props RegisteredCompetitiors
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns>Saves DB</returns>
        public async Task AddCompetitionAssociation(RegisteredCompetitor registeredCompetitor)
        {
            var competitions = await _context.Competitions.ToListAsync();

            foreach (Competition competition in competitions)
            {
                if (competition.ID == registeredCompetitor.CompetitionID)
                {
                    competition.RegisteredCompetitors.Add(registeredCompetitor);
                    _context.Competitions.Update(competition);
                }
            }
            _context.SaveChanges();      
        }

        /// <summary>
        /// Removes RegisteredCompetitor from Competition Nav Props RegisteredCompetitiors
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns>Saves DB</returns>
        public async Task RemoveCompetitionAssociation(RegisteredCompetitor registeredCompetitor)
        {
            var competitions = await _context.Competitions.ToListAsync();

            foreach (Competition competition in competitions)
            {
                if (competition.ID == registeredCompetitor.CompetitionID)
                {
                    competition.RegisteredCompetitors.Remove(registeredCompetitor);
                    _context.Competitions.Update(competition);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Adds RegisteredCompetitor to Participant Nav Props RegisteredCompetitiors
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        public async Task AddParticipantAssociation(RegisteredCompetitor registeredCompetitor)
        {
            var participants = await _context.Participants.ToListAsync();

            foreach (Participant participant in participants)
            {
                if (participant.ID == registeredCompetitor.ParticipantID)
                {
                    participant.RegisteredCompetitors.Add(registeredCompetitor);
                    _context.Participants.Update(participant);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes RegisteredCompetitor from Participant Nav Props RegisteredCompetitiors
        /// </summary>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        public async Task RemoveParticipantAssociation(RegisteredCompetitor registeredCompetitor)
        {
            var participants = await _context.Participants.ToListAsync();

            foreach (Participant participant in participants)
            {
                if (participant.ID == registeredCompetitor.ParticipantID)
                {
                    participant.RegisteredCompetitors.Remove(registeredCompetitor);
                    _context.Participants.Update(participant);
                }
            }
            _context.SaveChanges();
        }

        public SelectList ListValidCompetitors()
        {

            return new SelectList(_context.Participants, "ID", "ID");
        }

        public async Task<IEnumerable<Competition>> ListCompetitions()
        {
            var competitions = from c in _context.Competitions
                                   select c;
            return await competitions.ToListAsync();
        }
    }
}
