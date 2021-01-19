using Microsoft.EntityFrameworkCore;
using UnitTestDemo.Models;

namespace UnitTestDemo
{
    public class BikeDbContext : DbContext
    {
        public BikeDbContext(DbContextOptions<BikeDbContext> options) : base(options)
        {
        }

        public DbSet<Bike> Bikes { get; set; }
    }
}
