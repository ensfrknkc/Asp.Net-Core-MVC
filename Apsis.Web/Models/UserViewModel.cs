using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Ad bilgisi girilmelidir!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad bilgisi girilmelidir!")]
        public string Surname { get; set; }
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentificationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Gerçersiz E-Mail adresi!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre girilmesi zorunludur!")]
        public string Password { get; set; }
    }
}
