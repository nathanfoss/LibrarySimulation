using Books.Domain;
using Books.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Events
{
    public class BookBorrowEventService : IBookBorrowEventService
    {
        private readonly BookDbContext context;

        public BookBorrowEventService(BookDbContext context)
        {
            this.context = context;
        }

        public async Task Add(BookBorrowEvent bookBorrowEvent)
        {
            context.Events.Add(bookBorrowEvent);
            await context.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<BookBorrowEvent> bookBorrowEvents)
        {
            context.Events.AddRange(bookBorrowEvents);
            await context.SaveChangesAsync();
        }

        public async Task Delete(BookBorrowEvent bookBorrowEvent)
        {
            context.Events.Remove(bookBorrowEvent);
            await context.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<BookBorrowEvent> bookBorrowEvents)
        {
            context.Events.RemoveRange(bookBorrowEvents);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookBorrowEvent>> GetAll()
        {
            return await context.Events.ToListAsync();
        }
    }
}
