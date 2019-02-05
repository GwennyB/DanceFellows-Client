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
            RegisteredCompetitor testCompetitor1 = new RegisteredCompetitor();
            RegisteredCompetitor testCompetitor2 = new RegisteredCompetitor();
            List<RegisteredCompetitor> competitorList = new List<RegisteredCompetitor>() { testCompetitor1, testCompetitor2 };
            Competition testComp = new Competition { ID = 1, CompType = CompType.JackAndJill, Level = Level.Newcomer, RegisteredCompetitors = competitorList };
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
            RegisteredCompetitor testCompetitor1 = new RegisteredCompetitor();
            RegisteredCompetitor testCompetitor2 = new RegisteredCompetitor();
            List<RegisteredCompetitor> competitorList = new List<RegisteredCompetitor>() { testCompetitor1, testCompetitor2 };
            Assert.Equal(competitorList, testComp.RegisteredCompetitors);
        }
    }
}
