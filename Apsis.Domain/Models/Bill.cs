using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Domain.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public virtual Flat Flat { get; set; }
        public float Amount { get; set; }
        public string BillType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
