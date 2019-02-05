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
    }
}
