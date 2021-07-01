using Apsis.Domain.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure.Repositories
{
    public class BlockRepository : Repository<Block> , IBlockRepository
    {
        ApsisDbContext _context;
        public BlockRepository(ApsisDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
