using Microsoft.EntityFrameworkCore;
using Records.Domain;
using Records.Domain.Borrowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records.Infrastructure.Borrowing
{
    public class BorrowingRecordService : IBorrowingRecordService
    {
        private readonly RecordsDbContext context;

        public BorrowingRecordService(RecordsDbContext context)
        {
            this.context = context;
        }

        public async Task Add(BorrowingRecord record)
        {
            context.BorrowingRecords.Add(record);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BorrowingRecord>> GetByBook(int bookId)
        {
            return await context
                .BorrowingRecords
                .Where(b => b.BookId == bookId)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowingRecord>> GetByPatron(int patronId)
        {
            return await context
                .BorrowingRecords
                .Where(b => b.PatronId == patronId)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }
    }
}
