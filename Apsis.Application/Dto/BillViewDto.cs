using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class BillViewDto
    {
        public int Id { get; set; }
        public int FlatId { get; set; }
        public float Amount { get; set; }
        public string BillType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool Status { get; set; }
    }
}
