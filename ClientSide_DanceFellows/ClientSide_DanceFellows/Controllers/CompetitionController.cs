using ClientSide_DanceFellows.Models;
using ClientSide_DanceFellows.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly ICompetitionManager _context;

        public CompetitionController(ICompetitionManager context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Handles Competitions Index Page Load
        /// </summary>
        /// <returns>List of all existing Competitions</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetCompetitions());
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
        /// Once the submit button is pressed it will add new Competition to database and return user to Index page.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompType,Level")]Competition competition)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateCompetition(competition);

                return RedirectToAction(nameof(Index));
            }
            return View(competition);
        }

        /// <summary>
        /// Searches for Competition and returns if found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var competition = await _context.GetCompetition(id);

            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        /// <summary>
        /// Takes in a Competition and removes it from the DB
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Competition competition)
        {
            _context.DeleteCompetition(competition);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Enables the nav property so that registered competitors can be associated with a competition
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
