using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class MessageViewDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
