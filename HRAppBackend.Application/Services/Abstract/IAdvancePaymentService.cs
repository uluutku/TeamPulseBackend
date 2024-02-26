using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
	public interface IAdvancePaymentService : IBaseService<AdvancePayment>
	{
		Task<AdvancePayment> CreateAdvancePayment(string appUserId, CreateAdvancePaymentDTO createAdvancePaymentDTO);
		Task DecisionForProcess(string processId, bool processResult);
		Task DeleteAdvancePayment(int advanceId);
		Task<List<GetAdvancePaymentDTO>> GetAllAdvancePayments(string appUserId);
		Task UpdateAdvancePayment(string appUserId, int advanceId, UpdateAdvancePaymentDTO updateAdvancePaymentDTO);
	}
}
