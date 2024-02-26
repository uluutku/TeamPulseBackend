using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.LeaveDTO
{
	public class LeaveUpdateDTO
	{
		public string Type { get; set; }        
        public DateTime LeaveDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Description { get; set; }
	}
}
