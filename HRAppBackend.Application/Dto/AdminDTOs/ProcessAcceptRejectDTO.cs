using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.AdminDTOs
{
	public class ProcessAcceptRejectDTO
	{
        public string ProcessId { get; set; }
        public string ProcessType { get; set; }
        public bool ProcessResult { get; set; }
    }
}
