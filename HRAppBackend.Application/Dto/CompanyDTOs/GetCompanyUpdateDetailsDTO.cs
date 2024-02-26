using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.CompanyDTOs
{
	public class GetCompanyUpdateDetailsDTO
	{
        public string LogoPath { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }


	}
}
