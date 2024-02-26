using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.CompanyDTOs
{
	public class SubscriptionDTO
	{
        public int CompanyId { get; set; }
        public DateTime SubscriptionEnd { get; set; }
    }
}
