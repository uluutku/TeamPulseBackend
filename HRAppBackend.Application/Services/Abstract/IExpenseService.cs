using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface IExpenseService : IBaseService<Expense>
    {
        Task<Expense> CreateExpense(string appUserId, ExpenseDTO expenseDTO);
        Task DeleteExpense(int expenseId);
        Task UpdateExpense(string appUserId, int expenseId, UpdateExpanseDTO updateExpanseDTO);
        Task<List<GetExpenseDTO>> GetAllExpenses(string appUserId);
		Task DecisionForProcess(string processId, bool processResult);
	}
}
