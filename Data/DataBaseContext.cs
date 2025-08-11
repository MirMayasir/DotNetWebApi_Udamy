using Microsoft.EntityFrameworkCore;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Data
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options)
        {
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Regions
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    Id = 1,
                    Code = "IND",
                    Name = "India",
                    RegionImageUrl = "https://example.com/india.jpg"
                },
                new Region
                {
                    Id = 2,
                    Code = "USA",
                    Name = "United States",
                    RegionImageUrl = "https://example.com/usa.jpg"
                }
            );

            // Seed Difficulties
            modelBuilder.Entity<Difficulty>().HasData(
                new Difficulty { Id = 1, Name = "Easy" },
                new Difficulty { Id = 2, Name = "Medium" },
                new Difficulty { Id = 3, Name = "Hard" }
            );

            // Seed Walks
            modelBuilder.Entity<Walk>().HasData(
                new Walk
                {
                    Id = 1,
                    Name = "Sunset Trail",
                    Description = "A peaceful walk during sunset.",
                    LengthInKm = 4.2,
                    WalkImageUrl = "https://example.com/sunset.jpg",
                    RegionId = 1,
                    DifficultyId = 1
                },
                new Walk
                {
                    Id = 2,
                    Name = "Mountain Climb",
                    Description = "Challenging walk up the mountain.",
                    LengthInKm = 8.7,
                    WalkImageUrl = "https://example.com/mountain.jpg",
                    RegionId = 2,
                    DifficultyId = 3
                }
            );
        }
    }
}
