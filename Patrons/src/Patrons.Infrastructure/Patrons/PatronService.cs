using Patrons.Domain;
using Patrons.Domain.Patrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrons.Infrastructure.Patrons
{
    public class PatronService : IPatronService
    {
        private readonly PatronDbContext context;

        public PatronService(PatronDbContext context)
        {
            this.context = context;
        }

        public async Task<Patron> Add(Patron patron)
        {
            throw new NotImplementedException();
        }

        public async Task<Patron> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patron>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patron>> Search(string searchText)
        {
            throw new NotImplementedException();
        }

        public async Task<Patron> Update(Patron patron)
        {
            throw new NotImplementedException();
        }
    }
}
