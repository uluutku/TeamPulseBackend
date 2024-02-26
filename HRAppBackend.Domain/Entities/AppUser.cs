using HRAppBackend.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Domain.Entities
{
	public class AppUser : IdentityUser
	{
        public Employee Employee { get; set; }
        public int ConfirmCode { get; set; }

    }
}
