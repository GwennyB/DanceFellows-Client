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

namespace ClientSide_DanceFellows.Controllers
{
    public class RegisteredCompetitorController : Controller
    {
        private readonly IRegisteredCompetitorManager _context;

        public RegisteredCompetitorController(IRegisteredCompetitorManager context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: "Filter" by CompID or
        /// display all if null
        /// </summary>
        /// <param name="searchString">filter to apply</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.SearchRegisteredCompetitor(Convert.ToInt32(searchString)));
            }
            return View(await _context.GetRegisteredCompetitors());
        }

        /// <summary>
        /// GET: This will handle a person pressing the details button on the RegisteredCompetitors Index Page. It will open a view of the selected RegisteredCompetitor.
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="competitiorID"></param>
        /// <returns>RegisteredCompetitor</returns>
        public async Task<IActionResult> Details(int participantID, int competitiorID)
        {
            if (participantID == 0 || competitiorID == 0)
            {
                return NotFound();
            }

            var registeredCompetitor = await _context.GetRegisteredCompetitor(participantID, competitiorID);
            if (registeredCompetitor == null)
            {
                return NotFound();
            }

            return View(registeredCompetitor);
        }

        /// <summary>
        /// GET: Route user to Create view.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {

            ViewData["ParticipantID"] = new SelectList(await _context.ListValidCompetitors(), "ID", "LastName");
            ViewData["CompetitionID"] = new SelectList(await _context.ListCompetitions(), "ID", "CompType");


            return View();
        }

        /// <summary>
        /// Once the submit button is pressed it will add new RegisteredCompetitor and add RegisteredCompetitor to Competition and Participant nav props then save to database and return user to Index page.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipantID,CompetitionID,Role,Placement,BibNumber,ChiefJudgeScore,JudgeOneScore,JudgeTwoScore,JudgeThreeScore,JudgeFourScore,JudgeFiveScore,JudgeSixScore,Participant,Competition")] RegisteredCompetitor registeredCompetitor)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateRegisteredCompetitor(registeredCompetitor);
                await _context.AddCompetitionAssociation(registeredCompetitor);
                await _context.AddParticipantAssociation(registeredCompetitor);

                return RedirectToAction(nameof(Index));
            }
            return View(registeredCompetitor);
        }

        /// <summary>
        /// When edit is selected will redirect to a edit page with the RegisteredCompetitor information.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int participantID, int competitiorID)
        {
            if (participantID == 0 || competitiorID == 0)
            {
                return NotFound();
            }

            var registeredCompetitor = await _context.GetRegisteredCompetitor(participantID, competitiorID);
            if (registeredCompetitor == null)
            {
                return NotFound();
            }
            return View(registeredCompetitor);
        }

        /// <summary>
        /// When submit button is pressed will check if valid update nave props and will then update DB entry,
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="competitiorID"></param>
        /// <param name="registeredCompetitor"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int participantID, int competitiorID, [Bind("ParticipantID,CompetitionID,Role,Placement,BibNumber,ChiefJudgeScore,JudgeOneScore,JudgeTwoScore,JudgeThreeScore,JudgeFourScore,JudgeFiveScore,JudgeSixScore,Participant,Competition")] RegisteredCompetitor registeredCompetitor)
        {
            if (participantID != registeredCompetitor.ParticipantID || competitiorID != registeredCompetitor.CompetitionID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _context.RemoveCompetitionAssociation(registeredCompetitor);
                await _context.AddCompetitionAssociation(registeredCompetitor);

                await _context.RemoveParticipantAssociation(registeredCompetitor);
                await _context.AddParticipantAssociation(registeredCompetitor);

                _context.UpdateRegisteredCompetitor(registeredCompetitor);

                return RedirectToAction(nameof(Index));
            }
            return View(registeredCompetitor);
        }

        /// <summary>
        /// Searches for registeredCompetitor and returns if found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>registeredCompetitor</returns>
        public async Task<IActionResult> Delete(int participantID, int competitionID)
        {
            if (participantID == 0 || competitionID == 0)
            {
                return NotFound();
            }

            var registeredCompetitor = await _context.GetRegisteredCompetitor(participantID, competitionID);
            if (registeredCompetitor == null)
            {
                return NotFound();
            }
            return View(registeredCompetitor);
        }

        /// <summary>
        /// Takes in a registeredCompetitor and removes it from the DB and removes nav prop associations
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RegisteredCompetitor registeredCompetitor)
        {
            await _context.RemoveCompetitionAssociation(registeredCompetitor);
            await _context.RemoveParticipantAssociation(registeredCompetitor);
            _context.DeleteRegisteredCompetitor(registeredCompetitor);
            return RedirectToAction(nameof(Index));
        }


        private static HttpClient client = new HttpClient();
        private string path = "https://apidancefellows20190204115607.azurewebsites.net/api/";

        private async Task<IActionResult> CreateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            List<Object> data = new List<Object>();
            data.Add(reg.Participant);
            data.Add(reg);
            data.Add(reg.Competition);
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("Results/Create", data);
                response.EnsureSuccessStatusCode();
                Response.StatusCode = 200;
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return NotFound();
            }
        }

        private async Task<IActionResult> UpdateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            List<Object> data = new List<Object>();
            data.Add(reg.Participant);
            data.Add(reg);
            data.Add(reg.Competition);
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("Results/Update", data);
                response.EnsureSuccessStatusCode();
                Response.StatusCode = 200;
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return NotFound();
            }
        }

        private async Task<IActionResult> DeleteResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"Results/Delete/?EventID={reg.EventID}&CompetitorID={reg.ParticipantID}&CompType={reg.Competition.CompType}&Level={reg.Competition.Level}");
                response.EnsureSuccessStatusCode();
                Response.StatusCode = 200;
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return NotFound();
            }
        }


    }
}
