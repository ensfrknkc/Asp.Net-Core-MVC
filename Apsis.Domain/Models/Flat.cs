using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Domain.Models
{
    public class Flat
    {
        public int Id { get; set; }   
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual ICollection<Bill>  Bill { get; set; }
        public int BlockId { get; set; }
        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; }
        public bool Status { get; set; }
        public string NumberOfRooms { get; set; }
        public int Floor { get; set; }
        public int FlatNo { get; set; }
        public string OwnerUser { get; set; }
    }
}
