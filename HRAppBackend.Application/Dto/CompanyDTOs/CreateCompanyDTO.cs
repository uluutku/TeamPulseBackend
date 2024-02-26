using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.CompanyDTOs
{
	public class CreateCompanyDTO
	{
		public IFormFile CompanyLogo { get; set; }
		public string CompanyName { get; set; }
		public string CompanyDescription { get; set; }
		public DateTime SubscriptionStart { get; set; }
		public DateTime SubscriptionEnd { get; set; }
		public string CompanyAddress { get; set; }
		public string CompanyPhone { get; set; }
		public string AdminEmail { get; set; }
		public string AdminPassword { get; set; }
		public string AdminFirstName { get; set; }
		public string AdminLastName { get; set; }
		public string? AdminMiddleName { get; set; }
		public string? AdminSecondLastName { get; set; }
		public string AdminTRIdentityNumber { get; set; }
		public DateTime AdminBirthDate { get; set; }
		public string AdminPlaceOfBirth { get; set; }


	}
}
