using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apsis.Domain.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Message> Message { get; set; }
        public int IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Plate { get; set; }
    }
}
