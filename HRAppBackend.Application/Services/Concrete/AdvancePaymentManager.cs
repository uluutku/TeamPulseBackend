using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
	public class AdvancePaymentManager : BaseManager<AdvancePayment>, IAdvancePaymentService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IEmployeeService _employeeService;
		private readonly IAdvancePaymentRepository _advancePaymentRepository;

		public AdvancePaymentManager(IBaseRepository<AdvancePayment> baseRepository, UserManager<AppUser> userManager, IEmployeeService employeeService, IAdvancePaymentRepository advancePaymentRepository) : base(baseRepository)
		{
			_userManager = userManager;
			_employeeService = employeeService;
			_advancePaymentRepository = advancePaymentRepository;
		}

		public async Task<AdvancePayment> CreateAdvancePayment(string appUserId, CreateAdvancePaymentDTO createAdvancePaymentDTO)
		{
			try
			{
				Enum.TryParse(createAdvancePaymentDTO.Type, true, out AdvanceType type);
				Enum.TryParse(createAdvancePaymentDTO.Currency, true, out Currency currency);
				var user = await _userManager.FindByIdAsync(appUserId);
				if(user == null) { throw new Exception("Kullanıcı Bulunamadı!"); }
				var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);

				var advancePayment = new AdvancePayment
				{
					EmployeeId = employee.Id,
					AdvanceType = type,
					Description = createAdvancePaymentDTO.Description,
					Currency = currency,
					Amount = createAdvancePaymentDTO.Amount,
					ApprovalStatus = 0,
					CreatedDate= DateTime.Now,					 
				};

				await this.TAddAsync(advancePayment);
				return advancePayment;
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
				var advancePayment = await _advancePaymentRepository.GetByIdAsync(Convert.ToInt32(processId));

				if (processResult)
				{
					advancePayment.ApprovalStatus = ApprovalStatus.Approved;
				}
				else
				{
					advancePayment.ApprovalStatus = ApprovalStatus.Denied;
				}

				await _advancePaymentRepository.UpdateAsync(advancePayment);
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}
		}

		public async Task DeleteAdvancePayment(int advanceId)
		{
			try
			{
				AdvancePayment advancePayment = await this.TGetByWhereAsync(x => x.AdvancePaymentId == advanceId);
				if (advancePayment == null) { throw new Exception("Avans Bulunamadı!"); }
				await this.TDeleteAsync(advancePayment);
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}
			
		}

		public async Task<List<GetAdvancePaymentDTO>> GetAllAdvancePayments(string appUserId)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(appUserId);
				if (user == null) { throw new Exception("Kullanıcı bulunamadı!"); }
				var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);
				var advancePaymentList = await this.TGetAllAsync(x => x.EmployeeId == employee.Id);

				List<GetAdvancePaymentDTO> advancePaymentDTO = new();

                foreach (AdvancePayment advancePayments in advancePaymentList )
				{
					GetAdvancePaymentDTO getAdvancePaymentDTO = new(){
						AdvancePaymentId = advancePayments.AdvancePaymentId,
						AdvanceType = Enum.GetName(typeof(AdvanceType), advancePayments.AdvanceType),
						Amount = advancePayments.Amount,
						ApprovalStatus = Enum.GetName(typeof(ApprovalStatus), advancePayments.ApprovalStatus),
						Currency = Enum.GetName(typeof(Currency), advancePayments.Currency),
						Description= advancePayments.Description,
					};
                    advancePaymentDTO.Add(getAdvancePaymentDTO);
                }
				
				return advancePaymentDTO;
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}
			

		}

		public async Task UpdateAdvancePayment(string appUserId, int advanceId, UpdateAdvancePaymentDTO updateAdvancePaymentDTO)
		{

			try
			{
				Enum.TryParse(updateAdvancePaymentDTO.Type, true, out AdvanceType type);
				Enum.TryParse(updateAdvancePaymentDTO.Currency, true, out Currency currency);
				AdvancePayment advancePayment = await this.TGetByWhereAsync(x => x.AdvancePaymentId == advanceId);
				if (advancePayment == null) { throw new Exception("Avans Bulunamadı!"); }

				advancePayment.AdvanceType = type;
				advancePayment.Currency = currency;
				advancePayment.Description = updateAdvancePaymentDTO.Description;
				advancePayment.Amount = updateAdvancePaymentDTO.Amount;
				advancePayment.UpdatedDate = DateTime.Now;

				await this.TUpdateAsync(advancePayment);
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}		

		}


	}
}
