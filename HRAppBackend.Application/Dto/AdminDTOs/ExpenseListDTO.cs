using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdminDTOs
{
    public class ExpenseListDTO
    {
        public List<string> Expenses { get; set; }
        public List<string> Employees { get; set; }
    }
}
