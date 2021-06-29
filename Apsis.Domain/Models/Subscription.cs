using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public virtual Flat Flat { get; set; }
        public float Amount { get; set; }
        public bool Status { get; set; }

    }
}
