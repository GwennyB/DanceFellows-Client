using ClientSide_DanceFellows.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Data
{
    public class ClientSideDanceFellowsDbContext : DbContext
    {

        public ClientSideDanceFellowsDbContext(DbContextOptions<ClientSideDanceFellowsDbContext> options) : base(options)
        {

        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredCompetitor>().HasKey(rc => new { rc.CompetitionID, rc.ParticipantID });

            //Add Seeds here.
            modelBuilder.Entity<Competition>().HasData(
            new Competition
            {
                ID = 1,
                CompType = CompType.Classic,
                Level = Level.Novice
            }
            );

            modelBuilder.Entity<Participant>().HasData(
            new Participant
            {
                ID = 1,
                WSC_ID = 1234,
                FirstName = "JimBob",
                LastName = "Franklin",
                MinLevel = Level.Novice,
                MaxLevel = Level.Advanced
            }
            );
        }

        //Reference to different tables.
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<RegisteredCompetitor> RegisteredCompetitors { get; set; }
    }
}
