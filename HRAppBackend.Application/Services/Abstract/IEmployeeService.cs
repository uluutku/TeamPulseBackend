using HRAppBackend.Application.Dto.AdminDTOs;
using HRAppBackend.Application.Dto.CompanyDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface IEmployeeService : IBaseService<Employee>
    { 
       
        Task<UserDetailsDTO> GetUserDetailsAsync(string appUserId);
        Task<UpdateInfoDTO> GetUserUpdateDetailsAsync(string appUserId);
        Task<SummaryDTO> GetSummary(string appUserId);
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync(string appUserId);
        Task<string> Update(string appUserId, UpdateDTO updateDTO);

        Task<string> UpdateWithID(string Id, UpdateDTO updateDTO);

        Task AddPersonel(string appUserId, AddPersonelDTO addPersonelDTO);

		Task<bool> UpdatePasswordAsync(string userId, string oldPassword, string newPassword);
		public bool ArePasswordsMatching(string newPassword, string confirmPassword);
        Task<GetAllProcessDTO> GetAllProcess(string appUserId);
		Task<CompanyLogoDTO> GetCompanyLogo(string appUserId);
	}
}

