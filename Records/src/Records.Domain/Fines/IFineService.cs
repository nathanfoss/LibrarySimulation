namespace Records.Domain.Fines
{
    public interface IFineService
    {
        Task Add(Fine fine);

        Task Update(Fine fine);

        Task Update(IEnumerable<Fine> fines);

        Task<Fine> Get(int id);

        Task<IEnumerable<Fine>> GetUnpaidByPatron(int patronId);

        Task<IEnumerable<Fine>> GetAllByPatron(int patronId);
    }
}
