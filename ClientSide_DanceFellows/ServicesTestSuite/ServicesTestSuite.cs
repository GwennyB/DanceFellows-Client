using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientSide_DanceFellows.Data;
using ClientSide_DanceFellows.Models;
using ClientSide_DanceFellows.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Xunit;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;

namespace ServicesTestSuite
{
    public class ServicesTestSuite
    {
        public class CompetitionServicesTestSuite
        {
            public Competition CreateCompetition()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 1 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Competition testCompetition = new Competition() { ID = 1, CompType = CompType.Classic, Level = Level.Novice, RegisteredCompetitors = listRC };

                return testCompetition;
            }

            [Fact]
            public async void CanCreateCompetition()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("CreateCompetition").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = CreateCompetition();

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);

                    await competitionService.CreateCompetition(testCompetition);

                    var result = context.Competitions.FirstOrDefault(a => a.ID == testCompetition.ID);

                    Assert.Equal(testCompetition, result);
                }
            }

            [Fact]
            public async void CanDeleteCompetition()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("DeleteCompetition").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = CreateCompetition();

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);

                    await competitionService.CreateCompetition(testCompetition);
                    competitionService.DeleteCompetition(testCompetition);

                    var result = context.Competitions.FirstOrDefault(a => a.ID == testCompetition.ID);

                    Assert.Null(result);
                }
            }

            [Fact]
            public async void CanGetCompetition()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetCompetition").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = CreateCompetition();

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);

                    await competitionService.CreateCompetition(testCompetition);
                    Competition expected = context.Competitions.FirstOrDefault(a => a.ID == testCompetition.ID);
                    Competition actual = await competitionService.GetCompetition(testCompetition.ID);

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanGetCompetitions()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetCompetitions").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = CreateCompetition();

                    RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 2, ParticipantID = 2 };
                    List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                    listRC.Add(testRegisteredCompetitor);
                    Competition testCompetition2 = new Competition() { ID = 2, CompType = CompType.Classic, Level = Level.Novice, RegisteredCompetitors = listRC };

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);

                    await competitionService.CreateCompetition(testCompetition);
                    await competitionService.CreateCompetition(testCompetition2);
                    IEnumerable<Competition> expected = new List<Competition> { testCompetition, testCompetition2 };
                    IEnumerable<Competition> actual = await competitionService.GetCompetitions();

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanGetRegisteredCompetitors()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetRegisteredCompetitors2").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = CreateCompetition();

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);

                    await competitionService.CreateCompetition(testCompetition);

                    IEnumerable<RegisteredCompetitor> expected = testCompetition.RegisteredCompetitors;
                    IEnumerable<RegisteredCompetitor> actual = await competitionService.GetRegisteredCompetitors(testCompetition.ID);

                    Assert.Equal(expected, actual);
                }
            }
        }

        public class ParticipantServicesTestSuite
        {
            public Participant CreateParticipant()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 1 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Participant testParticipant = new Participant() { ID = 1, WSC_ID = 1, FirstName = "JimBob", LastName = "Franklin", MinLevel = Level.Novice, MaxLevel = Level.Advanced, EligibleCompetitor = true, RegisteredCompetitors = listRC };

                return testParticipant;
            }

            [Fact]
            public async void CanCreateParticipant()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("CreateParticipant").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Participant testParticipant = CreateParticipant();

                    ParticipantManagementService participantService = new ParticipantManagementService(context);

                    await participantService.CreateParticipant(testParticipant);

                    var result = context.Participants.FirstOrDefault(a => a.ID == testParticipant.ID);

                    Assert.Equal(testParticipant, result);
                }
            }

            [Fact]
            public async void CanDeleteParticipant()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("DeleteParticipant").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Participant testParticipant = CreateParticipant();

                    ParticipantManagementService participantService = new ParticipantManagementService(context);

                    await participantService.CreateParticipant(testParticipant);
                    participantService.DeleteParticipant(testParticipant);

                    var result = context.Participants.FirstOrDefault(a => a.ID == testParticipant.ID);

                    Assert.Null(result);
                }
            }

            [Fact]
            public async void CanGetParticipant()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetParticipant").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Participant testParticipant = CreateParticipant();

                    ParticipantManagementService participantService = new ParticipantManagementService(context);

                    await participantService.CreateParticipant(testParticipant);
                    Participant expected = context.Participants.FirstOrDefault(a => a.ID == testParticipant.ID);
                    Participant actual = await participantService.GetParticipant(testParticipant.ID);

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanGetParticipants()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetParticipants").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Participant testParticipant = CreateParticipant();

                    RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 2 };
                    List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                    listRC.Add(testRegisteredCompetitor);
                    Participant testParticipant1 = new Participant() { ID = 2, WSC_ID = 2, FirstName = "Ricky", LastName = "Bobby", MinLevel = Level.Novice, MaxLevel = Level.Advanced, EligibleCompetitor = true, RegisteredCompetitors = listRC };

                    ParticipantManagementService participantService = new ParticipantManagementService(context);

                    await participantService.CreateParticipant(testParticipant);
                    await participantService.CreateParticipant(testParticipant1);
                    IEnumerable<Participant> expected = new List<Participant> { testParticipant, testParticipant1 };
                    IEnumerable<Participant> actual = await participantService.GetParticipants();

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanGetRegisteredCompetitors()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetRegisteredCompetitors").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Participant testParticipant = CreateParticipant();

                    ParticipantManagementService participantService = new ParticipantManagementService(context);

                    await participantService.CreateParticipant(testParticipant);

                    IEnumerable<RegisteredCompetitor> expected = testParticipant.RegisteredCompetitors;
                    IEnumerable<RegisteredCompetitor> actual = await participantService.GetRegisteredCompetitors(testParticipant.ID);

                    Assert.Equal(expected, actual);
                }
            }
        }

        public class RegisteredCompetitorManagementServicesTestSuite
        {
            
            public Competition CreateCompetition()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 1 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Competition testCompetition = new Competition() { ID = 1, CompType = CompType.Classic, Level = Level.Novice, RegisteredCompetitors = listRC };

                return testCompetition;
            }

            public Competition CreateCompetition1()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 2, ParticipantID = 2 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Competition testCompetition = new Competition() { ID = 2, CompType = CompType.Classic, Level = Level.Novice, RegisteredCompetitors = listRC };

                return testCompetition;
            }

            public Participant CreateParticipant()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 3, ParticipantID = 13 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Participant testParticipant = new Participant() { ID = 1, WSC_ID = 1, FirstName = "JimBob", LastName = "Franklin", MinLevel = Level.Novice, MaxLevel = Level.Advanced, EligibleCompetitor = true, RegisteredCompetitors = listRC };

                return testParticipant;
            }

            public Participant CreateParticipant1()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { CompetitionID = 4, ParticipantID = 4 };
                List<RegisteredCompetitor> listRC = new List<RegisteredCompetitor>();
                listRC.Add(testRegisteredCompetitor);
                Participant testParticipant = new Participant() { ID = 2, WSC_ID = 1, FirstName = "Ricky", LastName = "Bobby", MinLevel = Level.Novice, MaxLevel = Level.Advanced, EligibleCompetitor = true, RegisteredCompetitors = listRC };

                return testParticipant;
            }

            public RegisteredCompetitor CreateRegisteredCompetitor()
            {
                RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { ParticipantID = 1, CompetitionID = 1, Role = Role.Lead, Placement = Placement.Position1, BibNumber = 100, ChiefJudgeScore = 9, JudgeOneScore = 9, JudgeTwoScore = 9, JudgeThreeScore = 9, JudgeFourScore = 9, JudgeFiveScore = 9, JudgeSixScore = 9, Participant = CreateParticipant(), Competition = CreateCompetition(), EventID = 1 };

                return testRegisteredCompetitor;
            }

            [Fact]
            public async void CanCreateRegisteredCompetitor()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("CreateRegisteredCompetitor").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);

                    var result = context.RegisteredCompetitors.FirstOrDefault(a => a.ParticipantID == testRegisteredCompetitor.ParticipantID && a.CompetitionID == testRegisteredCompetitor.CompetitionID);

                    Assert.Equal(testRegisteredCompetitor, result);
                }
            }

            [Fact]
            public async void DeleteRegisteredCompetitor()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("DeleteRegisteredCompetitor").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);
                    await registeredCompetitorService.DeleteRegisteredCompetitor(testRegisteredCompetitor);

                    var result = context.RegisteredCompetitors.FirstOrDefault(a => a.ParticipantID == testRegisteredCompetitor.ParticipantID && a.CompetitionID == testRegisteredCompetitor.CompetitionID);

                    Assert.Null(result);
                }
            }

            [Fact]
            public async void GetRegisteredCompetitor()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetRegisteredCompetitor").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);
                    var result = context.RegisteredCompetitors.FirstOrDefault(a => a.ParticipantID == testRegisteredCompetitor.ParticipantID && a.CompetitionID == testRegisteredCompetitor.CompetitionID);
                    RegisteredCompetitor actual = await registeredCompetitorService.GetRegisteredCompetitor(testRegisteredCompetitor.ParticipantID, testRegisteredCompetitor.CompetitionID);

                    RegisteredCompetitor expected = context.RegisteredCompetitors.FirstOrDefault(a => a.ParticipantID == testRegisteredCompetitor.ParticipantID && a.CompetitionID == testRegisteredCompetitor.CompetitionID);

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void GetRegisteredCompetitors()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetRegisteredCompetitorsTwo").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitor testRegisteredCompetitor1 = new RegisteredCompetitor { ParticipantID = 2, CompetitionID = 1, Role = Role.Lead, Placement = Placement.Position2, BibNumber = 101, ChiefJudgeScore = 8, JudgeOneScore = 8, JudgeTwoScore = 8, JudgeThreeScore = 8, JudgeFourScore = 8, JudgeFiveScore = 8, JudgeSixScore = 8, Participant = CreateParticipant1(), Competition = CreateCompetition1(), EventID = 1 };

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);
                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor1);
                    IEnumerable<RegisteredCompetitor> expected = new List<RegisteredCompetitor> { testRegisteredCompetitor, testRegisteredCompetitor1 };
                    IEnumerable<RegisteredCompetitor> actual = await registeredCompetitorService.GetRegisteredCompetitors();

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanSearchRegisteredCompetitor()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("SearchRegisteredCompetitor").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    Competition testCompetition = new Competition() { ID = 1, CompType = CompType.Classic, Level = Level.Novice };
                    Competition testCompetition2 = new Competition() { ID = 2, CompType = CompType.JackAndJill, Level = Level.Advanced };

                    RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { ParticipantID = 1, CompetitionID = 1, Role = Role.Lead, Placement = Placement.Position2, BibNumber = 101, ChiefJudgeScore = 8, JudgeOneScore = 8, JudgeTwoScore = 8, JudgeThreeScore = 8, JudgeFourScore = 8, JudgeFiveScore = 8, JudgeSixScore = 8, Competition = testCompetition};

                    RegisteredCompetitor testRegisteredCompetitor1 = new RegisteredCompetitor { ParticipantID = 2, CompetitionID = 1, Role = Role.Lead, Placement = Placement.Position2, BibNumber = 101, ChiefJudgeScore = 8, JudgeOneScore = 8, JudgeTwoScore = 8, JudgeThreeScore = 8, JudgeFourScore = 8, JudgeFiveScore = 8, JudgeSixScore = 8, Competition = testCompetition };

                    RegisteredCompetitor testRegisteredCompetitor2 = new RegisteredCompetitor { ParticipantID = 3, CompetitionID = 2, Role = Role.Lead, Placement = Placement.Position2, BibNumber = 101, ChiefJudgeScore = 8, JudgeOneScore = 8, JudgeTwoScore = 8, JudgeThreeScore = 8, JudgeFourScore = 8, JudgeFiveScore = 8, JudgeSixScore = 8, Competition = testCompetition2 };

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    CompetitionManagementService competitionService = new CompetitionManagementService(context);
                    await competitionService.CreateCompetition(testCompetition);
                    await competitionService.CreateCompetition(testCompetition2);


                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);
                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor1);
                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor2);


                    IEnumerable<RegisteredCompetitor> expected = new List<RegisteredCompetitor> { testRegisteredCompetitor, testRegisteredCompetitor1 };
                    IEnumerable<RegisteredCompetitor> actual = await registeredCompetitorService.SearchRegisteredCompetitor("class");

                    Assert.Equal(expected, actual);

                }
            }

            [Fact]
            public async void CanUpdateRegisteredCompetitor()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("UpdateRegisteredCompetitor").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    await registeredCompetitorService.CreateRegisteredCompetitor(testRegisteredCompetitor);

                    testRegisteredCompetitor.ChiefJudgeScore = 10;

                    await registeredCompetitorService.UpdateRegisteredCompetitor(testRegisteredCompetitor);

                    var result = context.RegisteredCompetitors.FirstOrDefault(a => a.ParticipantID == testRegisteredCompetitor.ParticipantID && a.CompetitionID == testRegisteredCompetitor.CompetitionID);

                    Assert.Equal(testRegisteredCompetitor, result);

                }
            }

            [Fact]
            public async void CanListValidCompetitors()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("ListValidCompetitors").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    ParticipantManagementService participantManagementService = new ParticipantManagementService(context);

                    Participant testParticipant = new Participant()
                    {
                        ID = 1,
                        WSC_ID = 1,
                        FirstName = "JimBob",
                        LastName = "Franklin",
                        MinLevel = Level.Novice,
                        MaxLevel = Level.Advanced,
                        EligibleCompetitor = true,
                    };

                    Participant testParticipant1 = new Participant()
                    {
                        ID = 2,
                        WSC_ID = 0,
                        FirstName = "Ricky",
                        LastName = "Bobby",
                        MinLevel = Level.Novice,
                        MaxLevel = Level.Advanced,
                        EligibleCompetitor = false,
                    };

                    await participantManagementService.CreateParticipant(testParticipant);
                    await participantManagementService.CreateParticipant(testParticipant1);

                    IEnumerable<Participant> expected = new List<Participant> { testParticipant };
                    IEnumerable<Participant> actual = await registeredCompetitorService.ListValidCompetitors();

                    Assert.Equal(expected, actual);

                }
            }

            [Fact]
            public async void CanListCompetitions()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("ListCompetitions").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    CompetitionManagementService competitionManagementService = new CompetitionManagementService(context);

                    Competition testCompetition = new Competition() { ID = 1, CompType = CompType.Classic, Level = Level.Novice };
                    Competition testCompetition2 = new Competition() { ID = 2, CompType = CompType.JackAndJill, Level = Level.Advanced };

                    await competitionManagementService.CreateCompetition(testCompetition);
                    await competitionManagementService.CreateCompetition(testCompetition2);

                    IEnumerable<Competition> expected = new List<Competition> { testCompetition, testCompetition2 };
                    IEnumerable<Competition> actual = await registeredCompetitorService.ListCompetitions();

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanShowParticipant()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("ShowParticipant").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    ParticipantManagementService participantManagementService = new ParticipantManagementService(context);

                    await participantManagementService.CreateParticipant(testRegisteredCompetitor.Participant);

                    Participant expected = testRegisteredCompetitor.Participant;

                    Participant actual = await registeredCompetitorService.ShowParticipant(testRegisteredCompetitor.Participant.ID);

                    Assert.Equal(expected, actual);
                }
            }

            [Fact]
            public async void CanShowCompetition()
            {
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("ShowCompetition").Options;

                using (ClientSideDanceFellowsDbContext context = new ClientSideDanceFellowsDbContext(options))
                {
                    RegisteredCompetitor testRegisteredCompetitor = CreateRegisteredCompetitor();

                    RegisteredCompetitorManagementService registeredCompetitorService = new RegisteredCompetitorManagementService(context);

                    CompetitionManagementService competitionManagementService = new CompetitionManagementService(context);

                    await competitionManagementService.CreateCompetition(testRegisteredCompetitor.Competition);

                    Competition expected = testRegisteredCompetitor.Competition;

                    Competition actual = await registeredCompetitorService.ShowCompetition(testRegisteredCompetitor.Competition.ID);

                    Assert.Equal(expected, actual);
                }
            }
        }    
    }       
}
