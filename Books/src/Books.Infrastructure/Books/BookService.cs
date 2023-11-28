using Books.Domain;
using Books.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Books
{
    public class BookService : IBookService
    {
        private readonly BookDbContext context;

        public BookService(BookDbContext context)
        {
            this.context = context;
        }

        public async Task<Book> Add(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Get(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await context.Books.Where(x => !x.IsDeleted).OrderBy(x => x.Title).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByAuthor(int authorId)
        {
            return await context.Books.Where(x => !x.IsDeleted && x.AuthorId == authorId).OrderBy(x => x.Title).ToListAsync();
        }

        public async Task<IEnumerable<Book>> Search(string searchText)
        {
            return await context.Books.Where(x => !x.IsDeleted && x.Title.Contains(searchText)).OrderBy(x => x.Title).ToListAsync();
        }

        public async Task<Book> Update(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return book;
        }
    }
}
