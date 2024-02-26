using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdvancePaymentDTOs
{
    public class GetAdvancePaymentDTO
    {
        public string FullName { get; set; }
        public int EmployeeID { get; set; }
		public int AdvancePaymentId { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string AdvanceType { get; set; }
        public decimal Amount { get; set; }
        public string ApprovalStatus { get; set; }
		public string ProcessType { get; set; }

	}
}
