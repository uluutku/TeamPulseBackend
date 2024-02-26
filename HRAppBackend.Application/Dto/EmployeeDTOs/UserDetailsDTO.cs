using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.EmployeeDTOs
{
    public class UserDetailsDTO
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TRIdentityNumber { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Status Status { get; set; }
        public string ProfessionName { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
