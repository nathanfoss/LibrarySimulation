using Books.Domain.Authors;
using Books.Domain.Books;
using Books.Domain.BookStatuses;
using Books.Domain.Borrows;
using Books.Domain.Genres;
using Microsoft.EntityFrameworkCore;

namespace Books.Domain
{
    public class BookDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Library");
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookStatus> BookStatuses { get; set; }

        public DbSet<BookBorrow> BookBorrows { get; set; }
    }
}