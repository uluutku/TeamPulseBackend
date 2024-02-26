using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Dto.LeaveDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdminDTOs
{
	public class GetAllProcessDTO
	{
        public List<GetAdvancePaymentDTO>? AdvancePayments { get; set; }
        public List<GetLeaveDTO>? Leaves { get; set; }
        public List<GetExpenseDTO>? Expenses { get; set; }

    }
}
