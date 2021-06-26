using Apsis.Domain.Interfaces;
using Apsis.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure
{
    public class UnitofWork : IUnitofWork
    {
        public IFlatRepository Flat { get; }
        private readonly ApsisDbContext _context;
        public UnitofWork(ApsisDbContext context, IFlatRepository flatRepository)
        {
            Flat = flatRepository;
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
