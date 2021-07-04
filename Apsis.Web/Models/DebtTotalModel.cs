using Apsis.Application.Dto;
using Apsis.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Models
{
    public class DebtTotalModel
    {
        public float BillTotal { get; set; }
        public float SubscriptionTotal { get; set; }
        public List<Bill> Bill { get; set; }
        public List<Subscription> Subscription { get; set; }
    }
}
