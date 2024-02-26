using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AccountDTOs
{
    public class ConfirmCodeDTO
    {
        public string SCode { get; set; }
        public string Email { get; set; }
    }
}
