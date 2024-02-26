using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Dto.CompanyDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Dto.LeaveDTO;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using HRAppBackend.Domain.Enums;
using HRAppBackend.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
	public class CompanyManager : BaseManager<Company>, ICompanyService
	{
		private readonly IPhotoService _photoService;
		private readonly ICompanyRepository _companyRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDepartmentService _departmentService;
		private readonly IProfessionService _professionService;
		private readonly IEmployeeService _employeeService;

		public CompanyManager(IBaseRepository<Company> baseRepository, IPhotoService photoService, ICompanyRepository companyRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IDepartmentService departmentService, IProfessionService professionService, IEmployeeService employeeService) : base(baseRepository)
		{
			_photoService = photoService;
			_companyRepository = companyRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_departmentService = departmentService;
			_professionService = professionService;
			_employeeService = employeeService;
		}

		public async Task<bool> CreateCompany(CreateCompanyDTO createCompanyDTO)
		{
			try
			{

				var comp = await _companyRepository.GetByWhereAsync(x=>x.CompanyName == createCompanyDTO.CompanyName);
				if (comp != null) { throw new Exception("Bu isimde şirket mevcut!"); }

				var logoFileName = _photoService.GenerateLogoFileName(createCompanyDTO.CompanyLogo.FileName, createCompanyDTO.CompanyName);
				var logoFileUrl = _photoService.UploadCompanyLogo(createCompanyDTO.CompanyLogo, createCompanyDTO.CompanyName, logoFileName);





				Company company = new Company
				{
					CompanyName = createCompanyDTO.CompanyName,
					CompanyDescription = createCompanyDTO.CompanyDescription,
					CompanyAddress = createCompanyDTO.CompanyAddress,
					CompanyPhone = createCompanyDTO.CompanyPhone,
					LogoName = logoFileName,
					LogoPath = logoFileUrl,

					SubscriptionStart = createCompanyDTO.SubscriptionStart,
					SubscriptionEnd = createCompanyDTO.SubscriptionEnd,



				};

				bool result;
				var result1 = await _companyRepository.AddAsync(company);

				var result2 = await CreateAdmin(createCompanyDTO, logoFileName, logoFileUrl);

				if (result1 && result2) { result = true; }
				else { result = false; }

				return result;
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}

		}

		private async Task<bool> CreateAdmin(CreateCompanyDTO createCompanyDTO, string logoFileName, string logoFileUrl)
		{
			try
			{
				var user = new AppUser
				{
					UserName = createCompanyDTO.AdminEmail,
					Email = createCompanyDTO.AdminEmail,
					PhoneNumber = "string",

					EmailConfirmed = false,

					ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
										  // Diğer gerekli özellikleri de burada set edebilirsiniz
				};

				// Kullanıcıyı veritabanına ekleyin


				await _userManager.CreateAsync(user, createCompanyDTO.AdminPassword); // Şifreyi güvenli bir şifre ile değiştirin


				AppUser createdUser = await _userManager.FindByNameAsync(createCompanyDTO.AdminEmail);
				IdentityRole role = await _roleManager.FindByNameAsync("Admin");


				await _userManager.AddToRoleAsync(createdUser, role.Name);





				// Employee örneği oluşturun
				var employee = new Employee
				{
					FirstName = createCompanyDTO.AdminFirstName,
					LastName = createCompanyDTO.AdminLastName,
					Address = "Admin Adresi",


					MiddleName = createCompanyDTO.AdminMiddleName,
					SecondLastName = createCompanyDTO.AdminSecondLastName,
					City = "İSTANBUL",


					County = "KADIKÖY",
					BirthDate = createCompanyDTO.AdminBirthDate,
					PlaceOfBirth = createCompanyDTO.AdminPlaceOfBirth,
					FileName = logoFileName,
					FilePath = logoFileUrl,


					StartDate = DateTime.Now,
					ProfessionId = 1,
					DepartmentId = 1,


					TRIdentityNumber = createCompanyDTO.AdminTRIdentityNumber,


					Salary = 0M,


					Status = Domain.Enums.Status.Active,
					AppUserId = createdUser.Id,
					CompanyId = (await _companyRepository.GetByWhereAsync(x => x.CompanyName == createCompanyDTO.CompanyName)).CompanyId,

					// Diğer gerekli özellikleri de burada set edebilirsiniz
				};

				createdUser.Employee = employee;
				employee.AppUser = createdUser;

				await _employeeService.TAddAsync(employee);

				return true;
			}
			catch (Exception)
			{
				throw new Exception("Admin Ekleme Başarısız!");

			}


		}


		public async Task DeleteCompany(int companyId)
		{
			try
			{
				Company company = await this.TGetByWhereAsync(x => x.CompanyId == companyId);
				if (company == null)
				{
					throw new Exception("Sirket bulunamadi!");
				}

				var employeeList = await _employeeService.TGetAllAsync(x=> x.CompanyId == companyId);


				await this.TDeleteAsync(company);

				foreach (var employee in employeeList)
				{
					var user = await _userManager.FindByIdAsync(employee.AppUserId);
					await _userManager.DeleteAsync(user);
				}



			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}
		}


		public async Task<List<GetCompanysDTO>> GetAllCompanys()
		{
			var companies = await _companyRepository.GetAllAsync();
			var companyDtos = new List<GetCompanysDTO>();

			foreach (var company in companies)
			{
				var employees = await _employeeService.TGetAllAsync(x => x.CompanyId == company.CompanyId);
				Employee admin = null;

				foreach (var employee in employees)
				{
					var appUser = await _userManager.FindByIdAsync(employee.AppUserId);
					var result = await _userManager.IsInRoleAsync(appUser, "Admin");
					var result2 = await _userManager.IsInRoleAsync(appUser, "SiteAdmin");
					if (result || result2)
					{
						admin = employee;
						break;
					}
				}

				var adminUser = await _userManager.FindByIdAsync(admin.AppUserId);

				var companyDto = new GetCompanysDTO
				{
					CompanyName = company.CompanyName,
					Description = company.CompanyDescription,
					PhoneNumber = company.CompanyPhone,
					FullName = admin.FirstName + " " + admin.LastName,
					Email = adminUser.Email,
					EmployeeCount = employees.Count(),
					SubscriptionEnd = company.SubscriptionEnd,
					SubscriptionStart = company.SubscriptionStart,
					LogoPath = company.LogoPath,
					CompanyId = company.CompanyId
				};

				companyDtos.Add(companyDto);
			}

			return companyDtos;
		}

		public async Task StopSubscription(int companyId)
		{
			try
			{
				var company = await _companyRepository.GetByIdAsync(companyId);

				if (company == null) { throw new Exception("Şirket Bulunamadı!"); }

				company.SubscriptionEnd = DateTime.Now;
				await _companyRepository.UpdateAsync(company);
			}
			catch (Exception)
			{
				throw new Exception("İşlem Başarısız!");
			}

		}

		public async Task ExtendSubscription(SubscriptionDTO subscriptionDTO)
		{
			try


			{


				var company = await _companyRepository.GetByIdAsync(subscriptionDTO.CompanyId);
				if (company == null) { throw new Exception("Şirket Bulunamadı!"); }

				company.SubscriptionEnd = subscriptionDTO.SubscriptionEnd;
				await _companyRepository.UpdateAsync(company);
			}
			catch (Exception)

			{

				throw new Exception("İşlem Başarısız!");

			}

		}

		public async Task<GetCompanyUpdateDetailsDTO> GetCompanyUpdateDetails(int companyId)


		{


			try
			{
				var company = await _companyRepository.GetByIdAsync(companyId);
				if (company == null) { throw new Exception("Şirket Bulunamadı!"); }

				var companyDto = new GetCompanyUpdateDetailsDTO();

				companyDto.LogoPath = company.LogoPath;
				companyDto.CompanyName = company.CompanyName;
				companyDto.Description = company.CompanyDescription;
				companyDto.SubscriptionStart = company.SubscriptionStart;
				companyDto.SubscriptionEnd = company.SubscriptionEnd;
				companyDto.PhoneNumber = company.CompanyPhone;
				companyDto.Address = company.CompanyAddress;

				return companyDto;

			}

			catch (Exception ex)

			{

				throw new Exception(ex.Message);

			}

		}

		public async Task<bool> UpdateCompany(int companyId, UpdateCompanyDTO updateCompanyDTO)
		{
			try
			{
				var logoUrl = "";
				var company = await _companyRepository.GetByIdAsync(companyId);
				if (company == null)
				{
					throw new Exception("Islem basarisiz");
				}
				if (updateCompanyDTO.CompanyLogo != null)
				{
					logoUrl = _photoService.UploadFile(updateCompanyDTO.CompanyLogo, companyId.ToString());
					company.LogoPath = logoUrl;
					company.LogoName = company.CompanyName + "/" + companyId.ToString() + "/" + updateCompanyDTO.CompanyLogo.FileName;
				}

				company.CompanyName = updateCompanyDTO.CompanyName;
				company.CompanyDescription = updateCompanyDTO.CompanyDescription;
				company.CompanyAddress = updateCompanyDTO.CompanyAddress;
				company.CompanyPhone = updateCompanyDTO.CompanyPhone;
				company.SubscriptionEnd = updateCompanyDTO.SubscriptionEndDate;
				company.SubscriptionStart = updateCompanyDTO.SubscriptionStartDate;

				var result = await _companyRepository.UpdateAsync(company);
				return result;
			}
			catch (Exception)
			{
				throw new Exception("Islem basarisiz");
			}



		}




	}
}


