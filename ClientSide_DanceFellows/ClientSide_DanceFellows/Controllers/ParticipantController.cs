using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientSide_DanceFellows.Data;
using ClientSide_DanceFellows.Models;
using ClientSide_DanceFellows.Models.Interfaces;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ClientSide_DanceFellows.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantManager _context;

        public ParticipantController(IParticipantManager context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Handles Participants Index Page Load
        /// </summary>
        /// <returns>List of all existing Participants</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetParticipants());
        }

        /// <summary>
        /// GET: Route user to Create view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// When WSC ID is entered and submit is pressed it will create new participant based off of data received from API.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WSC_ID")] Participant participant)
        {
            Participant createdParticipant = await GetCompetitor(participant.WSC_ID);
            if(createdParticipant.WSC_ID > 0)
            {
                await _context.CreateParticipant(createdParticipant);

                return RedirectToAction(nameof(Index));
            }
                
            return View(participant);
        }

        /// <summary>
        /// Searches for Participant and returns if found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Participant</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var participant = await _context.GetParticipant(id);

            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        /// <summary>
        /// Takes in a Competition and removes it from the Clientside DB.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Participant participant)
        {
            _context.DeleteParticipant(participant);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Enables the nav property so that registered competitors can be associated with a participant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of registered competitiors</returns>
        public async Task<IEnumerable<RegisteredCompetitor>> ShowRegisteredCompetitors(int id)
        {
            var registeredCompetitors = await _context.GetRegisteredCompetitors(id);
            return registeredCompetitors;
        }

        /// <summary>
        /// Gets a competitor with the given WSCD_ID, if they exist.
        /// </summary>
        /// <param name="WSCD_ID">The ID to find a participant by.</param>
        /// <returns>If a competitor exists, returns that competitor as a participant. If not, returns an empty competitor.</returns>
        private static async Task<Participant> GetCompetitor(int WSCD_ID)
        {
            //Using https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client for the GET route
            var client = new HttpClient();
            string path = "https://dancefellowsapi.azurewebsites.net/";
            string pathExtension = "Competitors/GetCompetitor/" + WSCD_ID;

            Participant retrievedParticipant = new Participant();
            try
            {
                var rawParticipantPackage = await client.GetAsync(path + pathExtension);
                //Type type = rawParticipant.GetType();
                if (rawParticipantPackage.IsSuccessStatusCode)
                {
                    HttpContent rawParticipant = rawParticipantPackage.Content;
                    JObject rawParticipantObject = rawParticipant.ReadAsAsync<JObject>().Result;
                    if (rawParticipantObject["id"] != null && rawParticipantObject["wsdC_ID"] != null && rawParticipantObject["firstName"] != null && rawParticipantObject["lastName"] != null && rawParticipantObject["minLevel"] != null && rawParticipantObject["maxLevel"] != null)
                    {
                        retrievedParticipant.ID = (int)rawParticipantObject["id"];
                        retrievedParticipant.WSC_ID = (int)rawParticipantObject["wsdC_ID"];
                        retrievedParticipant.FirstName = (string)rawParticipantObject["firstName"];
                        retrievedParticipant.LastName = (string)rawParticipantObject["lastName"];
                        int minLevel = (int)rawParticipantObject["minLevel"];
                        int maxLevel = (int)rawParticipantObject["maxLevel"];
                        retrievedParticipant.MinLevel = (Level)minLevel;
                        retrievedParticipant.MaxLevel = (Level)maxLevel;
                        if (WSCD_ID > 0)
                        {
                            retrievedParticipant.EligibleCompetitor = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retrievedParticipant;
        }
    }
}
