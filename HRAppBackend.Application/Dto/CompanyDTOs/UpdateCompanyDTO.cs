using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.CompanyDTOs
{
    public class UpdateCompanyDTO
    {
        public IFormFile? CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        
    }
}
