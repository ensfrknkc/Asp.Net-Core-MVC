using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class SubscriptionViewDto
    {
        public int Id { get; set; }
        public int FlatId { get; set; }
        public float Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool Status = true;
    }
}
