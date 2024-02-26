using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Domain.Entities
{
    public class Company
    {
        public Company()
        {
            Employees = new List<Employee>();            
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyPhone { get; set; }
        public string? LogoPath { get; set; }
        public string? LogoName { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }

		public int RemainingSubscriptionDays
		{
			get
			{
				TimeSpan remainingTime = SubscriptionEnd - DateTime.Now;
				return (int)Math.Ceiling(remainingTime.TotalDays);
			}
		}

		//Nav. Props
		public List<Employee> Employees { get; set; }
    }
}
