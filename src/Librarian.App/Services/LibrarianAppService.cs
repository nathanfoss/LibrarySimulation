using Bogus;
using Books.Client.BookBorrows;
using Books.Client.Books;
using Books.Domain.Books;
using Books.Domain.Genres;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patrons.Client.Patrons;

namespace Librarian.App.Services
{
    public class LibrarianAppService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly Faker genericFaker = new();

        private Faker<Book> bookFaker;

        private const int bookCount = 100;

        private const int patronCount = 5;

        public LibrarianAppService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            SetupBookFaker();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var bookClient = scope.ServiceProvider.GetRequiredService<IBookClient>();
            var bookBorrowClient = scope.ServiceProvider.GetRequiredService<IBookBorrowClient>();
            var patronClient = scope.ServiceProvider.GetRequiredService<IPatronClient>();

            await SetupBookCatalog(bookClient, bookCount);
            var books = await bookClient.GetAll();

            var book = books.First();
            var author = book.AuthorId;
            var booksByAuthor = bookClient.GetByAuthor(author);

            var searchText = book.Title[..(book.Title.Length / 2)];
            var searchResults = await bookClient.Search(searchText);

            await SetupPatrons(patronClient, patronCount);
            var patrons = await patronClient.GetAll();

            var patron = patrons.First();
            await bookClient.Reserve(book.Id, patron.Id);
            await bookClient.Reserve(books.Last().Id, patron.Id);
            await bookBorrowClient.BorrowBook(book.Id, patron.Id);
            // TODO: View borrowings for that book
            // TODO: View borrowing records for that book
            // TODO: Search for a patron
            // TODO: View a patron
            // TODO: View a patrons borrowing records
            // TODO: View a patrons fines
            // TODO: View a patrons overdue books
            // TODO: Delete a book
        }

        private void SetupBookFaker()
        {
            bookFaker = new Faker<Book>()
                .RuleFor(x => x.Title, f => f.Lorem.Sentence())
                .RuleFor(x => x.GenreId, f => (GenreEnum)f.Random.Number(1, 8))
                .RuleFor(x => x.PublishDate, f => f.Date.Past(15));
        }

        private async Task SetupBookCatalog(IBookClient bookClient, int count)
        {
            var books = bookFaker.Generate(count);
            foreach (var book in books)
            {
                await bookClient.Add(book, genericFaker.Person.FullName);
            }
        }

        private async Task SetupPatrons(IPatronClient patronClient, int count)
        {
            var names = Enumerable.Range(0, count).Select(_ => genericFaker.Person.FullName);
            foreach (var name in names)
            {
                await patronClient.Add(name);
            }
        }
    }
}
