using HRAppBackend.Application.Dto.CompanyDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
	public interface ICompanyService : IBaseService<Company>
	{
		Task<bool> CreateCompany(CreateCompanyDTO createCompanyDTO);
		Task DeleteCompany(int companyId);
		Task ExtendSubscription(SubscriptionDTO subscriptionDTO);
		Task<List<GetCompanysDTO>> GetAllCompanys();
		Task<GetCompanyUpdateDetailsDTO> GetCompanyUpdateDetails(int companyId);
		Task StopSubscription(int companyId);
		Task<bool> UpdateCompany(int companyId, UpdateCompanyDTO updateCompanyDTO);
	}
}
