using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Domain.Enums;
using HRAppBackend.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
    public class ExpenseManager : BaseManager<Expense>, IExpenseService
    {
        private readonly UserManager<AppUser> _userManager;
		private readonly IExpenseRepository _expenseRepository;
		private readonly IEmployeeService _employeeService;

        public ExpenseManager(IBaseRepository<Expense> baseRepository, IEmployeeService employeeService, UserManager<AppUser> userManager, IExpenseRepository expenseRepository) : base(baseRepository)
        {
            _userManager = userManager;
			_expenseRepository = expenseRepository;
			_employeeService = employeeService;
        }

        public async Task<Expense> CreateExpense(string appUserId, ExpenseDTO expenseDTO)
        {
            try
            {
                Enum.TryParse(expenseDTO.Type, true, out ExpenseType type);
                Enum.TryParse(expenseDTO.Currency, true, out Currency currency);
                var user = await _userManager.FindByIdAsync(appUserId);
                if (user == null) { throw new Exception("Kullanıcı Bulunamadı!"); }
                var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);

                var expense = new Expense
                {
                    EmployeeId = employee.Id,
                    Description = expenseDTO.Description,
                    Currency = currency,
                    ExpenseType = type,
                    ApprovalStatus = 0,
                    Amount = expenseDTO.Amount,
                    ExpenseDate = expenseDTO.ExpenseDate,
                    InvoicePath = expenseDTO.InvoicePath,
                    CreatedDate = DateTime.Now
                };

                await this.TAddAsync(expense);
                return expense;
            }
            catch (Exception)
            {
                throw new Exception("İşlem Başarısız!");
            }

        }

		public async Task DecisionForProcess(string processId, bool processResult)
		{
            try
            {
				var expens = await _expenseRepository.GetByIdAsync(Convert.ToInt32(processId));

				if (processResult)
				{
					expens.ApprovalStatus = ApprovalStatus.Approved;
				}
				else
				{
					expens.ApprovalStatus = ApprovalStatus.Denied;
				}

				await _expenseRepository.UpdateAsync(expens);
			}
            catch (Exception)
            {
                throw new Exception("İşlem Başarısız!");
            }
            


		}

		public async Task DeleteExpense(int expenseId)
        {
            try
            {
                Expense expense = await this.TGetByWhereAsync(x => x.Id == expenseId);
                if (expense == null) { throw new Exception("Harcama Bulunamadı!"); }
                await this.TDeleteAsync(expense);
            }
            catch (Exception)
            {
                throw new Exception("İşlem Başarısız!");
            }
        }

        public async Task<List<GetExpenseDTO>> GetAllExpenses(string appUserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(appUserId);
                if (user == null) { throw new Exception("Kullanıcı bulunamadı!"); }
                var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);
                var expenseList = await this.TGetAllAsync(x => x.EmployeeId == employee.Id);

                List<GetExpenseDTO> expenseDTOs = new();

                foreach (Expense expenses in expenseList)
                {
                    GetExpenseDTO getExpenseDTO = new()
                    {
                        ExpenseId = expenses.Id,
                        Description = expenses.Description,
                        Amount = expenses.Amount,
                        ApprovalStatus = Enum.GetName(typeof(ApprovalStatus), expenses.ApprovalStatus),
                        Currency = Enum.GetName(typeof(Currency), expenses.Currency),
                        Type = Enum.GetName(typeof(Currency), expenses.ExpenseType),
                        InvoicePath = expenses.InvoicePath,
                        ExpenseDate = expenses.ExpenseDate,
                    };
                    expenseDTOs.Add(getExpenseDTO);
                }

                return expenseDTOs;
            }
            catch (Exception)
            {
                throw new Exception("İşlem Başarısız!");
            }
        }

        public Task GetAllExpensesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateExpense(string appUserId, int expenseId, UpdateExpanseDTO updateExpanseDTO)
        {
            try
            {
                Enum.TryParse(updateExpanseDTO.Type, true, out ExpenseType type);
                Enum.TryParse(updateExpanseDTO.Currency, true, out Currency currency);
                Expense expense = await this.TGetByWhereAsync(x => x.Id == expenseId);
                if (expense == null) { throw new Exception("Harcama Bulunamadı!"); }

                expense.ExpenseType = type;
                expense.Currency = currency;
                expense.Description = updateExpanseDTO.Description;
                expense.Amount = updateExpanseDTO.Amount;
                expense.InvoicePath = updateExpanseDTO.InvoicePath;
                expense.UpdatedDate = DateTime.Now;

                await this.TUpdateAsync(expense);
            }
            catch (Exception)
            {
                throw new Exception("İşlem Başarısız!");
            }
        }
    }
}
