using Microsoft.EntityFrameworkCore;
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

    public class Hotel
    {
        public string Name { get; set; }

        public int StarRating { get; set; }

        public string ResortCode { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string Timestamp { get; set; }

        public string ETag { get; set; }
    }
}
