using Microsoft.EntityFrameworkCore;
using Records.Domain.Borrowing;
using Records.Domain.Fines;

namespace Records.Domain
{
    public class RecordsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Records");
        }

        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        public DbSet<BorrowingRecordType> BorrowingRecordTypes { get; set; }

        public DbSet<Fine> Fines { get; set; }
    }
}
