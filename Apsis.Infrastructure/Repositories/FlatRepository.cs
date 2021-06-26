using Apsis.Domain.Models;
using Apsis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apsis.Infrastructure.Context;

namespace Apsis.Infrastructure.Repositories
{
    public class FlatRepository : Repository<Flat>, IFlatRepository
    {
        public FlatRepository(ApsisDbContext context) : base(context)
        {

        }
    }
}
