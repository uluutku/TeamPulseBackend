using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.TokenDTOs
{
    public class TokenDTO
    {
        public string AccressToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
