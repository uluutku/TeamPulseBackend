using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Dto.LeaveDTO;
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
    public class LeaveManager : BaseManager<Leave>, ILeaveService
    {

        private readonly ILeaveRepository _leaveRepository;
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;

        public LeaveManager(IBaseRepository<Leave> baseRepository, ILeaveRepository leaveRepository, IEmployeeService employeeService, UserManager<AppUser> userManager) : base(baseRepository)
        {
            _leaveRepository = leaveRepository;
            _employeeService = employeeService;
            _userManager = userManager;
        }

        public async Task<Leave> CreateLeave(string appUserId, LeaveDTO leaveDTO)
        {
            if (string.IsNullOrEmpty(appUserId) || leaveDTO == null)
            {
                throw new ArgumentException("Hatali giris, izin bilgileri boş olamaz.");
            }

            if (leaveDTO.LeaveDate == default || leaveDTO.DueDate == default)
            {
                throw new ArgumentException("İzin başlangıç veya bitiş tarihi boş olamaz.");
            }

            Enum.TryParse(leaveDTO.Type, true, out LeaveTypes type);
                   
            var user = await _userManager.FindByIdAsync(appUserId);
            if (user == null)
            {
                throw new Exception("Hatali giris, tekrar deneyiniz!");
            }

            var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);
            if (employee == null)
            {
                throw new Exception("Hatali giris, tekrar deneyiniz!");
            }

            var leave = new Leave
            {
                EmployeeId = employee.Id,
                Type = type,
                Status = ApprovalStatus.Waiting,
                LeaveDate = leaveDTO.LeaveDate,
                DueDate = leaveDTO.DueDate,                
                Description = leaveDTO.Description,
                RequestDate = DateTime.Now,
                NumberOfDays = (leaveDTO.DueDate - leaveDTO.LeaveDate).Days               
                
            };

            await _leaveRepository.AddAsync(leave);
            return leave;
        }

		public async Task DecisionForProcess(string processId, bool processResult)
		{
			try
			{
				var leave = await _leaveRepository.GetByIdAsync(Convert.ToInt32(processId));

				if (processResult)
				{
					leave.Status = ApprovalStatus.Approved;
				}
				else
				{
					leave.Status = ApprovalStatus.Denied;
				}

				await _leaveRepository.UpdateAsync(leave);
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}
		}

		public async Task Delete(string appUserId, int leaveId)
        {
            if (string.IsNullOrEmpty(appUserId))
            {
                throw new ArgumentException("Isleminize devam edilemiyor.");
            }

            var leave = await _leaveRepository.GetByWhereAsync(x => x.Id == leaveId) ;
            if (leave == null)
            {
                throw new ArgumentException("Islem basarisiz.");
            }
            await _leaveRepository.DeleteAsync(leave);
        }

        public async Task<List<GetLeaveForUserDTO>> GetLeavesByUserId(string appUserId)
        {
            if (string.IsNullOrEmpty(appUserId))
            {
                throw new ArgumentException("Isleminize devam edilemiyor.");
            }
           
            var leaves = await _leaveRepository.GetLeavesByUserIdAsync(appUserId);
            var leaveDTOs = leaves.Select(leave => new GetLeaveForUserDTO
            {
                LeaveID = leave.Id,
                RequestDate = leave.RequestDate,
                Type = leave.Type.ToString(),
                ApprovalStatus = leave.Status.ToString(),
                LeaveDate = leave.LeaveDate,
                DueDate = leave.DueDate,                
                Description = leave.Description,
                
            }).ToList();

            return leaveDTOs;
        }

        public async Task<string> Update(int leaveId, string appUserId, LeaveUpdateDTO leaveDTO)
        {
            if (string.IsNullOrEmpty(appUserId) || leaveDTO == null)
            {
                return "Isleme devam edilemiyor. Tekrar deneyiniz!";
            }

            if (leaveDTO.LeaveDate == default || leaveDTO.DueDate == default)
            {
                return "İzin başlangıç veya bitiş tarihi boş olamaz.";
            }


            var user = await _userManager.FindByIdAsync(appUserId);
            if (user == null)
            {
                return "Isleme devam edilemiyor. Tekrar deneyiniz!";
            }

            var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);
            if (employee == null)
            {
                return "Isleme devam edilemiyor. Tekrar deneyiniz!";
            }

            var leave = await _leaveRepository.GetByWhereAsync(x => x.Id == leaveId);
            if (leave == null)
            {
                return "Izin belgesi guncellenirken hata ile karsilasildi. Tekrar deneyiniz!";
            }

            Enum.TryParse(leaveDTO.Type, true, out LeaveTypes type);
            
            leave.Type = type;            
            leave.LeaveDate = leaveDTO.LeaveDate;
            leave.DueDate = leaveDTO.DueDate;
            leave.Description = leaveDTO.Description;
            leave.NumberOfDays = (leaveDTO.DueDate - leaveDTO.LeaveDate).Days;
            var result = await _leaveRepository.UpdateAsync(leave);
            if (result == null)
            {
                return "Izin belgesi guncellenirken hata ile karsilasildi. Tekrar deneyiniz!";
            }
            else
            {
                return "Izin belgesi basari ile guncellendi.";
            }
        }
    }
}
