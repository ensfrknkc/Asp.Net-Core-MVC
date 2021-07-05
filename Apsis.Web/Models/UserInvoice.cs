using Apsis.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Models
{
    public class UserInvoice
    {
        public List<Bill> Bills { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
