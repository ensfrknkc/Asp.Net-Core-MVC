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
        public IBillRepository Bill { get; }
        public IBlockRepository Block { get; }
        public IMessageRepository Message { get; }
        public ISubscriptionRepository Subscription { get; }
        public IUserRepository User { get; }

        private readonly ApsisDbContext _context;
        public UnitofWork(ApsisDbContext context, IFlatRepository flatRepository, IBillRepository billRepository, IBlockRepository blockRepository,
            IMessageRepository messageRepository,ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            Flat = flatRepository;
            Bill = billRepository;
            Block = blockRepository;
            Message = messageRepository;
            Subscription = subscriptionRepository;
            User = userRepository;
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
