using Apsis.Domain.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApsisDbContext context) : base(context)
        {

        }
    }
}
