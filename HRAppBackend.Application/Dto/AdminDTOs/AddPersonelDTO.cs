using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdminDTOs
{
    public class AddPersonelDTO
    {
        public IFormFile ProfilePicture { get; set; }

        public string Department { get; set; }
        public string Profession { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime StartDate { get; set; }
        public string TRIdentityNumber { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Phone { get; set; }

    }
}
