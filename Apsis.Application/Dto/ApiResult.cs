using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apsis.Application.Dto
{
    public class ApiResult
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
