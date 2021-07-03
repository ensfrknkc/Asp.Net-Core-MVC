using Apsis.Application.Dto;
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
        public List<BillViewDto> Bill { get; set; }
        public List<SubscriptionViewDto> Subscription { get; set; }
    }
}
