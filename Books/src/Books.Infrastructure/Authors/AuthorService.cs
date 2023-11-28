using Books.Domain;
using Books.Domain.Authors;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly BookDbContext context;

        public AuthorService(BookDbContext context)
        {
            this.context = context;
        }

        public async Task<Author> Add(Author author)
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Get(string fullName)
        {
            return await context.Authors.FirstOrDefaultAsync(a => a.FullName == fullName);
        }
    }
}
