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

        //TODO: Need to add a connection to API so that a user can be populated from existing data.


        /// <summary>
        /// GET: This will handle a person pressing the details button on the Participant Index Page. It will open a view of the selected participant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Competition</returns>
        public async Task<IActionResult> Details(int id)
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
        /// GET: Route user to Create view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Once the submit button is pressed it will add new Participant to database and return user to Index page.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WSC_ID,FirstName,LastName,MinLevel,MaxLevel")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                
                await _context.CreateParticipant(participant);

                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        /// <summary>
        /// When edit is selected will redirect to a edit page with the Participant information.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
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
        /// When submit button is pressed will check if valid and will then update DB entry,
        /// </summary>
        /// <param name="id"></param>
        /// <param name="participant"></param>
        /// <returns>Back to Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, WSC_ID, FirstName, LastName, MinLevel, MaxLevel")] Participant participant)
        {
            if (id != participant.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                
                _context.UpdateParticipant(participant);

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
        /// Takes in a Competition and removes it from the DB
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
    }
}
