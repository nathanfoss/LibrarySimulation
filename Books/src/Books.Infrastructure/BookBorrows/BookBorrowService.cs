using Books.Domain;
using Books.Domain.Borrows;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.BookBorrows
{
    public class BookBorrowService : IBookBorrowService
    {
        private readonly BookDbContext context;

        public BookBorrowService(BookDbContext context)
        {
            this.context = context;
        }

        public async Task<BookBorrow> Get(int id)
        {
            return await context.BookBorrows.FindAsync(id);
        }

        public async Task<BookBorrow> Add(BookBorrow bookBorrow)
        {
            context.BookBorrows.Add(bookBorrow);
            await context.SaveChangesAsync();
            return bookBorrow;
        }

        public async Task<IEnumerable<BookBorrow>> GetByBook(int bookId)
        {
            return await context.BookBorrows.Where(x => x.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<BookBorrow>> GetOverdue()
        {
            var now = DateTime.Now.Date;
            return await context.BookBorrows
                .Include(x => x.Book)
                .Where(x => !x.IsClosed && x.ExpirationDate < now)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookBorrow>> GetOverdue(int patronId)
        {
            var now = DateTime.Now.Date;
            return await context.BookBorrows
                .Include(x => x.Book)
                .Where(x => !x.IsClosed && x.PatronId == patronId && x.ExpirationDate < now)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookBorrow>> GetByPatron(int patronId)
        {
            return await context.BookBorrows
                .Include(x => x.Book)
                .Where(x => !x.IsClosed && x.PatronId == patronId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookBorrow>> GetAllByPatron(int patronId)
        {
            return await context.BookBorrows
                .Include(x => x.Book)
                .Where(x => x.PatronId == patronId)
                .ToListAsync();
        }

        public async Task<BookBorrow> Update(BookBorrow bookBorrow)
        {
            context.BookBorrows.Update(bookBorrow);
            await context.SaveChangesAsync();
            return bookBorrow;
        }
    }
}
