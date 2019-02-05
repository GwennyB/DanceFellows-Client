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
            //Add Seeds here.
        }

        //Reference to different tables.
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<RegisteredCompetitor> RegisteredCompetitors { get; set; }
    }
}
