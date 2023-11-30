using Microsoft.EntityFrameworkCore;
using Patrons.Domain.Patrons;

namespace Patrons.Domain
{
    public class PatronDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Patrons");
        }

        public DbSet<Patron> Patrons { get; set; }
    }
}
