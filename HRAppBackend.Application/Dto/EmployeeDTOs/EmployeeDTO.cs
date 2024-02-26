using HRAppBackend.Domain.Entities;
using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public string EmployeeId { get; set; }
        public string FilePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }
        public string Profession { get; set; }


    }
}
