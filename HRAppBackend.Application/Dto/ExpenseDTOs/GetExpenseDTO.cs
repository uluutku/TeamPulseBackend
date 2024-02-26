using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.ExpenseDTOs
{
	public class GetExpenseDTO
	{
        public string FullName { get; set; }
        public int EmployeeID { get; set; }
		public int ExpenseId { get; set; }
		public string Type { get; set; }
		public DateTime ExpenseDate { get; set; }
		public string Description { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }
		public string InvoicePath { get; set; }
        public string ApprovalStatus { get; set; }
        public string ProcessType { get; set; }
	}
}
