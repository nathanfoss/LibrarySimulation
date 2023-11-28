namespace Books.Domain.Authors
{
    public interface IAuthorService
    {
        Task<Author> Add(Author author);

        Task<Author> Get(string fullName);
    }
}
