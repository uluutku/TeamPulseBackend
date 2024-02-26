using HRAppBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.LeaveDTO
{
	public class GetLeaveDTO
	{
        public string FullName { get; set; }
        public int EmployeeID { get; set; }
		public int LeaveID { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		public DateTime LeaveDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Description { get; set; }
		public DateTime RequestDate { get; set; }
		public string ProcessType { get; set; }
	}
}
