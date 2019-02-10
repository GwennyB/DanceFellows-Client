using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models.Interfaces
{
    public interface IParticipantManager
    {
        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        Task CreateParticipant(Participant participant);

        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Participant> GetParticipant(int id);

        /// <summary>
        /// Read ALL Participants
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Participant>> GetParticipants();

        /// <summary>
        /// Read ALL Participants
        /// </summary>
        /// <param name="participant"></param>
        void DeleteParticipant(Participant participant);

        //Navagation Properties

        /// <summary>
        /// Read ALL Participants
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id);

    }
}
