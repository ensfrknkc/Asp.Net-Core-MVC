using Apsis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure
{
    public interface IUnitofWork
    {
        IFlatRepository Flat { get; }

        Task<int> SaveChangesAsync();
    }
}
