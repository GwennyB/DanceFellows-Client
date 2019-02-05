using ClientSide_DanceFellows.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TableTestSuite
{
    public class CompetitionTests
    {

        public Competition CreateComp()
        {
            RegisteredCompetitor testCompetitor1 = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 1 };
            RegisteredCompetitor testCompetitor2 = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 2 };
            List<RegisteredCompetitor> competitorList = new List<RegisteredCompetitor>() { testCompetitor1, testCompetitor2 };
            ICollection<RegisteredCompetitor> competitors = competitorList;
            Competition testComp = new Competition { ID = 1, CompType = CompType.JackAndJill, Level = Level.Newcomer, RegisteredCompetitors = competitors };
            return testComp;
        }

        [Fact]
        public void TestIDSet()
        {
            Competition testComp = new Competition();
            testComp.ID = 1;
            Assert.Equal(1, testComp.ID);
        }

        [Fact]
        public void TestIDGet()
        {
            Competition testComp = CreateComp();
            Assert.Equal(1, testComp.ID);
        }

        [Fact]
        public void TestCompTypeSet()
        {
            Competition testComp = new Competition();
            testComp.CompType = CompType.Classic;
            Assert.Equal(CompType.Classic, testComp.CompType);
        }

        [Fact]
        public void TestCompTypeGet()
        {
            Competition testComp = CreateComp();
            Assert.Equal(CompType.JackAndJill, testComp.CompType);
        }

        [Fact]
        public void TestLevelSet()
        {
            Competition testComp = new Competition();
            testComp.Level = Level.Novice;
            Assert.Equal(Level.Novice, testComp.Level);
        }

        [Fact]
        public void TestLevelGet()
        {
            Competition testComp = CreateComp();
            Assert.Equal(Level.Newcomer, testComp.Level);
        }

        [Fact]
        public void TestRegisteredCompetitorsSet()
        {
            Competition testComp = new Competition();
            RegisteredCompetitor testCompetitor1 = new RegisteredCompetitor();
            RegisteredCompetitor testCompetitor2 = new RegisteredCompetitor();
            List<RegisteredCompetitor> competitorList = new List<RegisteredCompetitor>() { testCompetitor1, testCompetitor2 };
            testComp.RegisteredCompetitors = competitorList;
            Assert.Equal(competitorList, testComp.RegisteredCompetitors);
        }

        [Fact]
        public void TestRegisteredCompetitorsGet()
        {
            Competition testComp = CreateComp();
            RegisteredCompetitor testCompetitor1 = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 1 };
            RegisteredCompetitor testCompetitor2 = new RegisteredCompetitor { CompetitionID = 1, ParticipantID = 2 };
            List<RegisteredCompetitor> testCompetitors = new List<RegisteredCompetitor>() { testCompetitor1, testCompetitor2 };

            ICollection<RegisteredCompetitor> expected = testCompetitors;
            ICollection<RegisteredCompetitor> actual = testComp.RegisteredCompetitors;

            RegisteredCompetitor[] expectedArray = new RegisteredCompetitor[expected.Count];
            expected.CopyTo(expectedArray, 0);

            RegisteredCompetitor[] actualArray = new RegisteredCompetitor[actual.Count];
            actual.CopyTo(actualArray, 0);

            Assert.Equal(expectedArray, actualArray);
        }
    }

    public class ParticipantTests
    {
        public Participant CreateParticipant()
        {
            Participant testParticipant = new Participant() {ID=1, WSC_ID=123, FirstName="First", LastName="Last", MinLevel=Level.Intermediate, MaxLevel=Level.Advanced };
            return testParticipant;
        }

        [Fact]
        public void TestIDSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.ID = 1;
            Assert.Equal(1, testParticipant.ID);
        }

        [Fact]
        public void TestIDGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal(1, testParticipant.ID);
        }

        [Fact]
        public void TestWSC_IDSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.WSC_ID = 1;
            Assert.Equal(1, testParticipant.WSC_ID);
        }

        [Fact]
        public void TestWSC_IDGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal(123, testParticipant.WSC_ID);
        }

        [Fact]
        public void TestFirstNameSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.FirstName = "Jane";
            Assert.Equal("Jane", testParticipant.FirstName);
        }

        [Fact]
        public void TestFirstNameGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal("First", testParticipant.FirstName);
        }

        [Fact]
        public void TestLastNameSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.LastName = "Doe";
            Assert.Equal("Doe", testParticipant.LastName);
        }

        [Fact]
        public void TestLastNameGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal("Last", testParticipant.LastName);
        }

        [Fact]
        public void TestMinLevelSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.MinLevel = Level.Newcomer;
            Assert.Equal(Level.Newcomer, testParticipant.MinLevel);
        }

        [Fact]
        public void TestMinLevelGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal(Level.Intermediate, testParticipant.MinLevel);
        }

        [Fact]
        public void TestMaxLevelSet()
        {
            Participant testParticipant = new Participant();
            testParticipant.MaxLevel = Level.AllStar;
            Assert.Equal(Level.AllStar, testParticipant.MaxLevel);
        }

        [Fact]
        public void TestMaxLevelGet()
        {
            Participant testParticipant = CreateParticipant();
            Assert.Equal(Level.Advanced, testParticipant.MaxLevel);
        }

        //TODO: Nav property tests
    }

    public class RegisteredCompetitorTests
    {
        public RegisteredCompetitor CreateRegisteredCompetitor()
        {
            RegisteredCompetitor testRegisteredCompetitor = new RegisteredCompetitor { ParticipantID = 1, CompetitionID = 1, Role = Role.Lead, Placement = Placement.Position1, BibNumber = 100, ChiefJudgeScore = 9, JudgeOneScore = 9, JudgeTwoScore = 9, JudgeThreeScore = 9, JudgeFourScore = 9, JudgeFiveScore = 9, JudgeSixScore = 9 };
            return testRegisteredCompetitor;
        }

        [Fact]
        public void ParticipantIDSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.ParticipantID = 1;
            Assert.Equal(1, testRegComp.ParticipantID);
        }

        [Fact]
        public void ParticipantIDGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(1, testRegComp.ParticipantID);
        }

        [Fact]
        public void CompetitionIDSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.CompetitionID = 1;
            Assert.Equal(1, testRegComp.CompetitionID);
        }

        [Fact]
        public void CompetitionIDGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(1, testRegComp.CompetitionID);
        }

        [Fact]
        public void RoleSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.Role = Role.Follow;
            Assert.Equal(Role.Follow, testRegComp.Role);
        }

        [Fact]
        public void RoleGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(Role.Lead, testRegComp.Role);
        }

        [Fact]
        public void PlacementSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.Placement = Placement.Finalled;
            Assert.Equal(Placement.Finalled, testRegComp.Placement);
        }

        [Fact]
        public void PlacementGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(Placement.Position1, testRegComp.Placement);
        }

        [Fact]
        public void BibNumberSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.BibNumber = 1234;
            Assert.Equal(1234, testRegComp.BibNumber);
        }

        [Fact]
        public void BibNumberGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(100, testRegComp.BibNumber);
        }

        [Fact]
        public void ChiefJudgeScoreSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.ChiefJudgeScore = 8;
            Assert.Equal(8, testRegComp.ChiefJudgeScore);
        }

        [Fact]
        public void ChiefJudgeScoreGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(9, testRegComp.ChiefJudgeScore);
        }

        [Fact]
        public void JudgeOneScoreSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.JudgeOneScore = 8;
            Assert.Equal(8, testRegComp.JudgeOneScore);
        }

        [Fact]
        public void JudgeOneScoreGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(9, testRegComp.JudgeOneScore);
        }

        [Fact]
        public void JudgeTwoScoreSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.JudgeTwoScore = 8;
            Assert.Equal(8, testRegComp.JudgeTwoScore);
        }

        [Fact]
        public void JudgeTwoScoreGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(9, testRegComp.JudgeTwoScore);
        }

        [Fact]
        public void JudgeThreeScoreSet()
        {
            RegisteredCompetitor testRegComp = new RegisteredCompetitor();
            testRegComp.JudgeThreeScore = 8;
            Assert.Equal(8, testRegComp.JudgeThreeScore);
        }

        [Fact]
        public void JudgeThreeScoreGet()
        {
            RegisteredCompetitor testRegComp = CreateRegisteredCompetitor();
            Assert.Equal(9, testRegComp.JudgeThreeScore);
        }
    }
}
