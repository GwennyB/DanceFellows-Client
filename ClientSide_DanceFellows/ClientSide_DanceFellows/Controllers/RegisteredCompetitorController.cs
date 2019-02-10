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
using Newtonsoft.Json;

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
        /// GET: Will filter by comptype and or level.
        /// display all if null
        /// </summary>
        /// <param name="searchString">filter to apply</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CompetitionID"] = new SelectList(await _context.ListCompetitions(), "ID", "CompetitionName");
            if (!String.IsNullOrEmpty(searchString))
            {
                var registeredCompetitorsSearch = await _context.SearchRegisteredCompetitor(searchString);
                foreach (RegisteredCompetitor registeredCompetitor in registeredCompetitorsSearch)
                {
                    Competition competiton = await _context.ShowCompetition(registeredCompetitor.CompetitionID);
                    registeredCompetitor.Competition = competiton;

                    Participant participant = await _context.ShowParticipant(registeredCompetitor.ParticipantID);
                    registeredCompetitor.Participant = participant;
                }
                return View(registeredCompetitorsSearch);
            }
            var registeredCompetitors = await _context.GetRegisteredCompetitors();

            foreach (RegisteredCompetitor registeredCompetitor in registeredCompetitors)
            {
                Competition competiton = await _context.ShowCompetition(registeredCompetitor.CompetitionID);
                registeredCompetitor.Competition = competiton;

                Participant participant = await _context.ShowParticipant(registeredCompetitor.ParticipantID);
                registeredCompetitor.Participant = participant;
            }

            return View(registeredCompetitors);
        }

        /// <summary>
        /// GET: Routes user to Create view.
        /// </summary>
        /// <returns>Create View</returns>
        public async Task<IActionResult> Create()
        {

            ViewData["ParticipantID"] = new SelectList(await _context.ListValidCompetitors(), "ID", "FullName");
            ViewData["CompetitionID"] = new SelectList(await _context.ListCompetitions(), "ID", "CompetitionName");


            return View();
        }

        /// <summary>
        /// POST: Once the submit button is pressed it will add new RegisteredCompetitor to Clientside and API DB then will redirect to Index page.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipantID,CompetitionID,Role,Placement,BibNumber,ChiefJudgeScore,JudgeOneScore,JudgeTwoScore,JudgeThreeScore,JudgeFourScore,JudgeFiveScore,JudgeSixScore,Participant,Competition")] RegisteredCompetitor registeredCompetitor)
        {
            var checkDuplicate = await _context.GetRegisteredCompetitor(registeredCompetitor.ParticipantID, registeredCompetitor.CompetitionID);
            if (checkDuplicate != null)
            {
                return View(checkDuplicate);
            }

            if (ModelState.IsValid)
            {
                await _context.CreateRegisteredCompetitor(registeredCompetitor);

                await CreateResult(registeredCompetitor);
                return RedirectToAction(nameof(Index));
            }
            
            return View(registeredCompetitor);
        }


        /// <summary>
        /// POST: Receives properties arrays when Score button is pressed and runs a for loop to create a registered competitor from those results then updates existing registered competitor with updated properties and saves to clientside db. Then updates API Db.
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="competitionID"></param>
        /// <param name="role"></param>
        /// <param name="bibNumber"></param>
        /// <param name="chiefJudgeScore"></param>
        /// <param name="judgeOneScore"></param>
        /// <param name="judgeTwoScore"></param>
        /// <param name="judgeThreeScore"></param>
        /// <param name="judgeFourScore"></param>
        /// <param name="judgeFiveScore"></param>
        /// <param name="judgeSixScore"></param>
        /// <returns>Then returns back to directors dashboard.</returns>
        [HttpPost, ActionName("Score")]
        public async Task<IActionResult> Score(int[] participantID, int[] competitionID, Role[] role, int[] bibNumber, int[] chiefJudgeScore, int[] judgeOneScore, int[] judgeTwoScore, int[] judgeThreeScore, int[] judgeFourScore, int[] judgeFiveScore, int[] judgeSixScore)
        {
            for (int i = 0; i < participantID.Length; i++)
            {
                RegisteredCompetitor registeredCompetitor = new RegisteredCompetitor();

                registeredCompetitor.ParticipantID = participantID[i];

                registeredCompetitor.CompetitionID = competitionID[i];

                registeredCompetitor.EventID = 1;

                registeredCompetitor.Role = role[i];

                registeredCompetitor.BibNumber = bibNumber[i];

                registeredCompetitor.ChiefJudgeScore = chiefJudgeScore[i];

                registeredCompetitor.JudgeOneScore = judgeOneScore[i];

                registeredCompetitor.JudgeTwoScore = judgeTwoScore[i];

                registeredCompetitor.JudgeThreeScore = judgeThreeScore[i];

                registeredCompetitor.JudgeFourScore = judgeFourScore[i];

                registeredCompetitor.JudgeFiveScore = judgeFiveScore[i];

                registeredCompetitor.JudgeSixScore = judgeSixScore[i];

                if (ModelState.IsValid)
                {
                    await _context.UpdateRegisteredCompetitor(registeredCompetitor);                  
                    await UpdateResult(registeredCompetitor);                 
                }
            }

            return RedirectToAction("Index", "Home");
        }
        

        /// <summary>
        /// Searches for registeredCompetitor and if found will load Delete page with received registered competitior.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete page with registered competitor,</returns>
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
        /// Takes in a registeredCompetitor and removes it from the clientside DB and removes it from the API db.
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RegisteredCompetitor registeredCompetitor)
        {
            await _context.DeleteRegisteredCompetitor(registeredCompetitor);
            await DeleteResult(registeredCompetitor);
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public async Task<IActionResult> CallAPI()
        //{
        //    RegisteredCompetitor test = new RegisteredCompetitor();
        //    test.ParticipantID = 12;
        //    test.CompetitionID = 7;
        //    test.EventID = 2;
        //    test.Role = Role.Lead;
        //    test.Placement = Placement.Position1;
        //    test.BibNumber = 150;
        //    test.ChiefJudgeScore = 1;
        //    test.JudgeOneScore = 1;
        //    test.JudgeTwoScore = 1;
        //    test.JudgeTwoScore = 1;
        //    test.JudgeThreeScore = 1;
        //    test.JudgeFourScore = 1;
        //    test.JudgeFiveScore = 1;
        //    test.JudgeSixScore = 1;

        //    return await DeleteResult(test);
        //}
        //private string path = "http://localhost:57983/";

        
        private static HttpClient client = new HttpClient();
        private string path = "https://apidancefellows20190204115607.azurewebsites.net/";

        /// <summary>
        /// Creates a new API DB entry from input Registered Competitor.
        /// </summary>
        /// <param name="reg"></param>
        /// <returns>Status code</returns>
        private async Task<IActionResult> CreateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }

            List<object> data = await BuildRegistrationObject(reg);

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(path+"Results/Create", data);
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
        
        /// <summary>
        /// Updates an existing API RegisteredCompetitor.
        /// </summary>
        /// <param name="reg"></param>
        /// <returns>Response Code</returns>
        private async Task<IActionResult> UpdateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }

            List<object> data = await BuildRegistrationObject(reg);

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(path+"Results/Update", data);
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

        /// <summary>
        /// Serialized RegisteredCompetitor into JSON object.
        /// </summary>
        /// <param name="reg"></param>
        /// <returns>JSON Object</returns>
        private async Task<List<object>> BuildRegistrationObject(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return null;
            }
            List<object> data = new List<object>();

            Competition competition = await _context.ShowCompetition(reg.CompetitionID);
            reg.Competition = competition;

            Participant participant = await _context.ShowParticipant(reg.ParticipantID);
            reg.Participant = participant;

            competition.RegisteredCompetitors = null;
            reg.Competition = null;
            reg.Participant = null;
            participant.RegisteredCompetitors = null;

            string package = JsonConvert.SerializeObject(competition) + " | " +
                            JsonConvert.SerializeObject(reg) + " | " +
                            JsonConvert.SerializeObject(participant);

            data.Add(competition);
            data.Add(participant);
            data.Add(reg);
            return data;
        }

        /// <summary>
        /// Removes and existing API RegisteredCompetitor.
        /// </summary>
        /// <param name="reg"></param>
        /// <returns>Status Code</returns>
        private async Task<IActionResult> DeleteResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            List<object> data = await BuildRegistrationObject(reg);
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(path + "Results/Delete", data);
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

