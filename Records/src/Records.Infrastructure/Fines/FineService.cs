using Microsoft.EntityFrameworkCore;
using Records.Domain;
using Records.Domain.Fines;

namespace Records.Infrastructure.Fines
{
    public class FineService : IFineService
    {
        private readonly RecordsDbContext context;

        public FineService(RecordsDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Fine fine)
        {
            context.Fines.Add(fine);
            await context.SaveChangesAsync();
        }

        public async Task<Fine> Get(int id)
        {
            return await context.Fines.FindAsync(id);
        }

        public async Task<IEnumerable<Fine>> GetAllByPatron(int patronId)
        {
            return await context.Fines
                .Where(f => f.PatronId == patronId)
                .OrderByDescending(f => f.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Fine>> GetUnpaidByPatron(int patronId)
        {
            return await context.Fines
                .Where(f => f.PatronId == patronId && !f.IsPaid)
                .OrderByDescending(f => f.CreatedDate)
                .ToListAsync();
        }

        public async Task Update(Fine fine)
        {
            context.Fines.Update(fine);
            await context.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<Fine> fines)
        {
            context.Fines.UpdateRange(fines);
            await context.SaveChangesAsync();
        }
    }
}
