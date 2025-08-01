using Microsoft.EntityFrameworkCore;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Data
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
