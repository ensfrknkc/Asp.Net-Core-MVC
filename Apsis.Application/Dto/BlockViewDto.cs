using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class BlockViewDto
    {
        public int Id { get; set; }
        public List<FlatViewDto> Flats { get; set; }
        public string Name { get; set; }
    }
}
