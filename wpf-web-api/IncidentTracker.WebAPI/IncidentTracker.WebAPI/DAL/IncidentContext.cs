using System;
using IncidentTracker.BusinessEntitiy;
using Microsoft.EntityFrameworkCore;

namespace IncidentTracker.DAL
{
    public class IncidentContext:DbContext
    {
        public DbSet<Incident> incidents { get; set; }

        public DbSet<Location> locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=IncidentDB.db");

    }
}
