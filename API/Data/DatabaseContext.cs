﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resort>().HasKey(r => r.Code);

        }
    }

    public class Resort
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public bool BeginnerFriendly { get; set; }

        public bool HighAltitude { get; set; }

        public bool FamilyFriendly { get; set; }
    }
}
