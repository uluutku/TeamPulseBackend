using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdminDTOs
{
    public class ProfessionDepartmantListDTO
    {
        public List<string> Departments { get; set; }
        public List<string> Professions { get; set; }
    }
}
