using Books.Application.BookBorrows;
using Books.Domain.Borrows;
using MediatR;

namespace Books.Client.BookBorrows
{
    public class BookBorrowClient : IBookBorrowClient
    {
        private readonly IMediator mediator;

        public BookBorrowClient(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task BorrowBook(int bookId, int patronId)
        {
            await mediator.Send(new BorrowBookCommand
            {
                BookId = bookId,
                PatronId = patronId
            });
        }

        public async Task<IEnumerable<BookBorrow>> GetAllPatronBorrowedBooks(int patronId)
        {
            var result = await mediator.Send(new GetAllPatronBorrowedBooksQuery { PatronId = patronId });
            return result.Response;
        }

        public async Task<IEnumerable<BookBorrow>> GetOverdueBooks()
        {
            var result = await mediator.Send(new GetOverdueBooksQuery());
            return result.Response;
        }

        public async Task<IEnumerable<BookBorrow>> GetPatronBorrowedBooks(int patronId)
        {
            var result = await mediator.Send(new GetPatronBorrowedBooksQuery { PatronId = patronId });
            return result.Response;
        }

        public async Task<IEnumerable<BookBorrow>> GetPatronOverdueBooks(int patronId)
        {
            var result = await mediator.Send(new GetPatronOverdueBooksQuery { PatronId = patronId });
            return result.Response;
        }

        public async Task RenewBook(int bookBorrowId)
        {
            await mediator.Send(new RenewBookCommand { BookBorrowId = bookBorrowId });
        }
    }
}
