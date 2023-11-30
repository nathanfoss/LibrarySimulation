namespace Patrons.Domain.Patrons
{
    public interface IPatronService
    {
        Task<Patron> Get(int id);

        Task<IEnumerable<Patron>> GetAll();

        Task<IEnumerable<Patron>> Search(string searchText);

        Task<Patron> Add(Patron patron);

        Task<Patron> Update(Patron patron);
    }
}
