using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models.Interfaces
{
    public interface IParticipantManager
    {
        //Create Participant
        Task CreateParticipant(Participant participant);

        //Read An Participant
        Task<Participant> GetParticipant(int id);

        //Read ALL Participants
        Task<IEnumerable<Participant>> GetParticipants();

        //Update A Participant
        void UpdateParticipant(Participant participant);

        //Delete A Participant
        void DeleteParticipant(Participant participant);

        void DeleteParticipant(int id);

        //Search Participants
        Task<IEnumerable<Participant>> SearchParticipants(string lastName);

        //Navagation Properties

        //Add NavProp to RegisteredCompetitors
        Task AddParticipantAssociation(Participant participant);

        //Remove NavProp from RegisteredCompetitors
        Task RemoveParticipantAssociation(Participant participant);


        //Read ALL RegisteredCompetitiors
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id);

    }
}
