using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Infrastructure
{
	public interface IPhotoService
	{
		public string UploadFile(IFormFile myFile, string id);
		public string GenerateFileName(string fileName, string EmployeeId);
		public string UploadCompanyLogo(IFormFile companyLogo, string companyName, string logoFileName);
		public string GenerateLogoFileName(string fileName, string EmployeeId);
	}
}
