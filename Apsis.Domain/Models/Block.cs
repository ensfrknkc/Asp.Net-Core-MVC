using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Domain.Models
{
    public class Block
    {
        public int Id { get; set; }
        public virtual ICollection<Flat> Flats { get; set; }
        public string Name { get; set; }
    }
}
