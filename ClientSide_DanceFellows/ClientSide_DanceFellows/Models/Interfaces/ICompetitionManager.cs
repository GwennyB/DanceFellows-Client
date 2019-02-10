using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models.Interfaces
{
    public interface ICompetitionManager
    {
        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        Task CreateCompetition(Competition competition);

        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Competition> GetCompetition(int id);

        /// <summary>
        /// Create Competition
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Competition>> GetCompetitions();

        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="competition"></param>
        void DeleteCompetition(Competition competition);

        //Navagation Properties

        /// <summary>
        /// Create Competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<RegisteredCompetitor>> GetRegisteredCompetitors(int id);

    }
}
