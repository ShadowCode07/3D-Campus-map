using Microsoft.EntityFrameworkCore;
using CampusMapAPI.Models;

namespace CampusMapAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Pin> Pins { get; set; }
        public DbSet<Media> Media { get; set; }

    }
}
