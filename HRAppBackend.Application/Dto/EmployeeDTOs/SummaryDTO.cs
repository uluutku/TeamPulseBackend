using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.EmployeeDTOs
{
    public class SummaryDTO
    {
        public string ImgFileName { get; set; }
        public string ImgFilePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? SecondLastName { get; set; }
        public string DepartmentName { get; set; }
        public string ProfessionName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
    }
}
