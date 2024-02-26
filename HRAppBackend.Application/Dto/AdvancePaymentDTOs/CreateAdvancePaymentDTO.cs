using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdvancePaymentDTOs
{
	public class CreateAdvancePaymentDTO
	{
		public string Type { get; set; }		
		public string Description { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }		
	}
}
