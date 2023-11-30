using Patrons.Domain.Patrons;

namespace Patrons.Client.Patrons
{
    public interface IPatronClient
    {
        Task<Patron> Get(int id);

        Task<IEnumerable<Patron>> GetAll();

        Task<IEnumerable<Patron>> Search(string searchText);

        Task<Patron> Add(string name);

        Task Deactivate(int id);
    }
}
