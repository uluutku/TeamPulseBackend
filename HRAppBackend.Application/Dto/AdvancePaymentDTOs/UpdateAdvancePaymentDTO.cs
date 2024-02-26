using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdvancePaymentDTOs
{
	public class UpdateAdvancePaymentDTO
	{
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }		
		public decimal Amount { get; set; }
		//public DateTime? UpdatedDate { get; set; }
	}
}
