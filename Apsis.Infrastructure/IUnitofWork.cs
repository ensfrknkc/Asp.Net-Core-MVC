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
        IBillRepository Bill { get; }
        IBlockRepository Block { get; }
        IMessageRepository Message { get; }
        ISubscriptionRepository Subscription { get; }
        IUserRepository User { get; }

        Task<int> SaveChangesAsync();
    }
}
