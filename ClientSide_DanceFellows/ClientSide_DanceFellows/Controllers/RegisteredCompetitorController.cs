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
        /// GET: "Filter" by CompID or
        /// display all if null
        /// </summary>
        /// <param name="searchString">filter to apply</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var registeredCompetitorsSearch = await _context.SearchRegisteredCompetitor(Convert.ToInt32(searchString));
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

            ViewData["ParticipantID"] = new SelectList(await _context.ListValidCompetitors(), "ID", "FullName");
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


        [HttpPost, ActionName("Score")]
        public async Task<IActionResult> Score(int[] participantID, int[] competitionID, Role[] role, int[] bibNumber, int[] chiefJudgeScore, int[] judgeOneScore, int[] judgeTwoScore, int[] judgeThreeScore, int[] judgeFourScore, int[] judgeFiveScore, int[] judgeSixScore)
        {
            for (int i = 0; i < participantID.Length; i++)
            {
                RegisteredCompetitor registeredCompetitor = new RegisteredCompetitor();

                registeredCompetitor.ParticipantID = participantID[i];

                registeredCompetitor.CompetitionID = competitionID[i];

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
                    _context.UpdateRegisteredCompetitor(registeredCompetitor);
                    //TODO: await UpdateResult(registeredCompetitor);
                }
            }

            return RedirectToAction(nameof(Index));
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
            _context.DeleteRegisteredCompetitor(registeredCompetitor);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<RegisteredCompetitor> CallAPI()
        {
            RegisteredCompetitor test = new RegisteredCompetitor();
            test.ParticipantID = 9;
            test.CompetitionID = 2;
            test.EventID = 1;
            test.Role = Role.Lead;
            test.Placement = Placement.Position1;
            test.BibNumber = 150;
            test.ChiefJudgeScore = 1;
            test.JudgeOneScore = 1;
            test.JudgeTwoScore = 1;
            test.JudgeTwoScore = 1;
            test.JudgeThreeScore = 1;
            test.JudgeFourScore = 1;
            test.JudgeFiveScore = 1;
            test.JudgeSixScore = 1;

            await CreateResult(test);
            return test;
        }


        private static HttpClient client = new HttpClient();
        private string path = "https://apidancefellows20190204115607.azurewebsites.net/";
        //private string path = "http://localhost:57983/";

        private async Task<IActionResult> CreateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            List<string> data = new List<string>();
           
            Competition competition = await _context.ShowCompetition(reg.CompetitionID);

            Participant participant = await _context.ShowParticipant(reg.ParticipantID);

            competition.RegisteredCompetitors = null;
            reg.Competition = null;
            reg.Participant = null;

            data.Add(JsonConvert.SerializeObject(competition));
            data.Add(JsonConvert.SerializeObject(reg));
            data.Add(JsonConvert.SerializeObject(participant));

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

        private async Task<IActionResult> UpdateResult(RegisteredCompetitor reg)
        {
            if (reg == null)
            {
                return NotFound();
            }
            List<string> data = new List<string>();
            data.Add(JsonConvert.SerializeObject(reg.Participant));
            data.Add(JsonConvert.SerializeObject(reg));
            data.Add(JsonConvert.SerializeObject(reg.Competition));
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

