using HRAppBackend.Application.Dto.LeaveDTO;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface ILeaveService : IBaseService<Leave>
    {
        Task<Leave> CreateLeave(string appUserId, LeaveDTO leaveDTO);
        Task<string> Update(int leaveId, string appUserId, LeaveUpdateDTO leaveDTO);
        Task Delete(string appUserId, int leaveId);
        Task<List<GetLeaveForUserDTO>> GetLeavesByUserId(string appUserId);
		Task DecisionForProcess(string processId, bool processResult);
	}
}
