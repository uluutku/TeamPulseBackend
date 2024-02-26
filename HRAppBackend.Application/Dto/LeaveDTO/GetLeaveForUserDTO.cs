using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.LeaveDTO
{
	public class GetLeaveForUserDTO
	{
		public int LeaveID { get; set; }
		public string Type { get; set; }
		public DateTime LeaveDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Description { get; set; }		
		public string ApprovalStatus { get; set; }		
		public DateTime RequestDate { get; set; }
		
	}
}
