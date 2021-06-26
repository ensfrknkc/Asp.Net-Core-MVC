using Apsis.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure.Context
{
    public class ApsisDbContext: IdentityDbContext<User>
    {
        public ApsisDbContext(DbContextOptions<ApsisDbContext> options) : base(options)
        {
        }
        //Identity User ı görmezse db set ile ekle
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Block> Blocks { get; set; }
    }
}
