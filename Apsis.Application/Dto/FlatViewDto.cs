using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class FlatViewDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SubscriptionId { get; set; }
        public List<BillViewDto>  Bills { get; set; }
        public int BlockId { get; set; }
        public bool Status { get; set; }
        public string NumberOfRooms { get; set; }
        public int Floor { get; set; }
        public int FlatNo { get; set; }
        public string OwnerUser { get; set; }
        public string BlockName { get; set; }
    }
}
