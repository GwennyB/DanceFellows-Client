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
                DbContextOptions<ClientSideDanceFellowsDbContext> options = new DbContextOptionsBuilder<ClientSideDanceFellowsDbContext>().UseInMemoryDatabase("GetRegisteredCompetitors").Options;

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
    }       
}
