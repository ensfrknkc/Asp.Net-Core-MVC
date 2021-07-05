using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public static class JwtToken
    {
        public static string Token { get; set; }
        public static DateTime ValidDate { get; set; }
    }
}
