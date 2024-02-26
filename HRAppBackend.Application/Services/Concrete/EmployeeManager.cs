using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using HRAppBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MailKit;
using HRAppBackend.Application.Dto.AdminDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Dto.LeaveDTO;
using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Domain.Enums;
using HRAppBackend.Application.Dto.CompanyDTOs;

namespace HRAppBackend.Application.Services.Concrete
{
    public class EmployeeManager : BaseManager<Employee>, IEmployeeService
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly IProfessionService _professionService;
		private readonly IDepartmentService _departmentService;
		private readonly IPhotoService _photoService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailService _emailService;
		private readonly IExpenseRepository _expenseRepository;
		private readonly IAdvancePaymentRepository _advancePaymentRepository;
		private readonly ILeaveRepository _leaveRepository;
		private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeManager(IBaseRepository<Employee> baseRepository, UserManager<AppUser> userManager, IProfessionService professionService, IDepartmentService departmentService, IPhotoService photoService, RoleManager<IdentityRole> roleManager, IEmailService emailService,IExpenseRepository expenseRepository,IAdvancePaymentRepository advancePaymentRepository, ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository, ICompanyRepository companyRepository) : base(baseRepository)
		{
			_userManager = userManager;
			_professionService = professionService;
			_departmentService = departmentService;
			_photoService = photoService;
			_roleManager = roleManager;
			_emailService = emailService;
			_expenseRepository = expenseRepository;
			_advancePaymentRepository = advancePaymentRepository;
			_leaveRepository = leaveRepository;
			_employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }
		public async Task<SummaryDTO> GetSummary(string appUserId)

		{

			if (string.IsNullOrEmpty(appUserId))
			{
				throw new KeyNotFoundException("Bir hata oluştu. Lütfen daha sonra tekrar deneyin!");
			}


			var summary = new SummaryDTO();

			AppUser user = await _userManager.FindByIdAsync(appUserId);

			if (user == null)
			{
				throw new KeyNotFoundException("Bir hata oluştu. Lütfen daha sonra tekrar deneyin!");
			}


			Employee employee = await this.TGetByWhereAsync(x => x.AppUserId == user.Id);

			if (employee == null)
			{
				throw new KeyNotFoundException("Bir hata oluştu. Lütfen daha sonra tekrar deneyin!");
			}


			Department department = await _departmentService.TGetByWhereAsync(x => x.Id == employee.DepartmentId);
			Profession profession = await _professionService.TGetByWhereAsync(x => x.Id == employee.ProfessionId);

			if (employee != null && user != null)
			{
				summary.ImgFileName = employee.FileName;
				summary.ImgFilePath = employee.FilePath;
				summary.FirstName = employee.FirstName;
				summary.LastName = employee.LastName;
				summary.MiddleName = employee.MiddleName;
				summary.SecondLastName = employee.SecondLastName;
				summary.DepartmentName = department.Name;
				summary.ProfessionName = profession.Name;
				summary.Email = user.Email;
				summary.PhoneNumber = user.PhoneNumber;
				summary.Address = employee.Address;
				summary.City = employee.City;
				summary.County = employee.County;
			}
			return summary;
		}

		public async Task<string> Update(string id, UpdateDTO updateDTO)
		{
			try
			{
				string fileUrl = "";
				string phoneRegexPattern = @"^05[0-9]{9}$";
				AppUser user = await _userManager.FindByIdAsync(id);
				Employee employee = await this.TGetByWhereAsync(x => x.AppUserId == user.Id);

				if (updateDTO.ImgFile != null)
				{
					fileUrl = _photoService.UploadFile(updateDTO.ImgFile, id);
					employee.FileName = user.UserName + "-image";
					employee.FilePath = fileUrl;
				}

				employee.Address = updateDTO.Address;
				employee.City = updateDTO.City;
				employee.County = updateDTO.County;
				await this.TUpdateAsync(employee);

				if (!Regex.IsMatch(updateDTO.PhoneNumber, phoneRegexPattern))
				{
					throw new KeyNotFoundException("Doğru Formatta telefon gir!");
				}
				else
				{
					user.PhoneNumber = updateDTO.PhoneNumber;
					await _userManager.UpdateAsync(user);
					return employee.FilePath;
				}
			}
			catch (Exception ex)
			{
				// Hatanın detaylarını direkt olarak döndürelim.
				return "Hata";
			}
		}

		public async Task<string> UpdateWithID(string id, UpdateDTO updateDTO)
		{
			try
			{
				string fileUrl = "";

				string phoneRegexPattern = @"^05[0-9]{9}$";
				AppUser user = await _userManager.FindByIdAsync(id);
				Employee employee = await this.TGetByWhereAsync(x => x.AppUserId == user.Id);

				if (updateDTO.ImgFile != null)
				{
					fileUrl = _photoService.UploadFile(updateDTO.ImgFile, id);
					employee.FileName = user.UserName + "-image";
					employee.FilePath = fileUrl;
				}

				employee.Address = updateDTO.Address;
				employee.City = updateDTO.City;
				employee.County = updateDTO.County;
				await this.TUpdateAsync(employee);

				if (!Regex.IsMatch(updateDTO.PhoneNumber, phoneRegexPattern))
				{
					throw new KeyNotFoundException("Doğru Formatta telefon gir!");
				}
				else
				{
					user.PhoneNumber = updateDTO.PhoneNumber;
					await _userManager.UpdateAsync(user);
					return employee.FilePath;
				}
			}
			catch (Exception ex)
			{
				// Hatanın detaylarını direkt olarak döndürelim.
				return "Hata";
			}
		}

		public async Task<UserDetailsDTO> GetUserDetailsAsync(string appUserId)
		{
			try
			{
				var summary = new SummaryDTO();
				AppUser user = await _userManager.FindByIdAsync(appUserId);
				Employee employee = await this.TGetByWhereAsync(x => x.AppUserId == user.Id);

				if (employee == null)
				{
					return null;
					throw new NullReferenceException("Hatali giris, lutfen tekrar deneyiniz!");
				}

				Department department = await _departmentService.TGetByWhereAsync(x => x.Id == employee.DepartmentId);
				Profession profession = await _professionService.TGetByWhereAsync(x => x.Id == employee.ProfessionId);

				return new UserDetailsDTO
				{
					Address = employee.Address,
					City = employee.City,
					County = employee.County,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					MiddleName = employee.MiddleName,
					SecondLastName = employee.SecondLastName,
					BirthDate = employee.BirthDate,
					PlaceOfBirth = employee.PlaceOfBirth,
					StartDate = employee.StartDate,
					EndDate = employee.EndDate,
					TRIdentityNumber = employee.TRIdentityNumber,
					FileName = employee.FileName,
					FilePath = employee.FilePath,
					Status = employee.Status,
					ProfessionName = profession?.Name, // Null check ekledim
					DepartmentName = department?.Name, // Null check ekledim
					PhoneNumber = user.PhoneNumber,
					Email = user.Email
				};
			}
			catch (Exception ex)
			{
				return null;
				throw new Exception("Bir hata ile karşılaşıldı, tekrar deneyiniz!");
			}
		}

		public async Task<UpdateInfoDTO> GetUserUpdateDetailsAsync(string appUserId)
		{
			try
			{
				var summary = new SummaryDTO();
				AppUser user = await _userManager.FindByIdAsync(appUserId);
				Employee employee = await this.TGetByWhereAsync(x => x.AppUserId == user.Id);

				if (employee == null)
				{
					return null;
					throw new NullReferenceException("Hatali giris, lutfen tekrar deneyiniz!");
				}

				Department department = await _departmentService.TGetByWhereAsync(x => x.Id == employee.DepartmentId);
				Profession profession = await _professionService.TGetByWhereAsync(x => x.Id == employee.ProfessionId);

				return new UpdateInfoDTO
				{
					Address = employee.Address,
					City = employee.City,
					County = employee.County,
					FilePath = employee.FilePath,
					FileName = employee.FileName,
					PhoneNumber = user.PhoneNumber
				};
			}
			catch (Exception ex)
			{
				return null;
				throw new Exception("Bir hata ile karşılaşıldı, tekrar deneyiniz!");
			}
		}

		public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync(string appUserId)
		{
			try
			{
				var appUser = await _userManager.FindByIdAsync(appUserId);
				var admin = await this.TGetByWhereAsync(x => x.AppUserId == appUser.Id);
				var employeesList = await this.TGetAllAsync(x => x.CompanyId == admin.CompanyId);
				var employeeDTOs = new List<EmployeeDTO>();

				foreach (var employee in employeesList)
				{
					var currentUser = await _userManager.FindByIdAsync(employee.AppUserId);
					var department = await _departmentService.TGetByWhereAsync(x => x.Id == employee.DepartmentId);
					var profession = await _professionService.TGetByIdAsync(employee.ProfessionId);

					employeeDTOs.Add(new EmployeeDTO
					{

						EmployeeId = currentUser.Id,
						FilePath = employee.FilePath,
						FirstName = employee.FirstName,
						LastName = employee.LastName,
						Email = currentUser?.Email,
						PhoneNumber = currentUser?.PhoneNumber,
						DepartmentName = department?.Name,
						Status = employee.Status.ToString(),
						Profession = profession.Name
					});
				}

				return employeeDTOs;
			}
			catch (Exception ex)
			{
				throw new KeyNotFoundException(ex.Message);
			}
		}

		public async Task AddPersonel(string appUserId, AddPersonelDTO addPersonelDTO)
		{
            try
            {
                var appUser = await _userManager.FindByIdAsync(appUserId);
                var admin = await this.TGetByWhereAsync(x => x.AppUserId == appUser.Id);
                var phoneNumberAvailable = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == addPersonelDTO.Phone);
                var trIdentityNumberAvailable = await this.TGetByWhereAsync(x => x.TRIdentityNumber == addPersonelDTO.TRIdentityNumber);
                if (phoneNumberAvailable != null)
                    throw new Exception("Bu telefon numarası mevcut");
                if (trIdentityNumberAvailable != null)
                    throw new Exception("Bu kimlik numarası mevcut");


                var adminEmailDomain = appUser.Email.Substring(appUser.Email.IndexOf('@'));
                string username = "";
                var userList = await _userManager.Users.ToListAsync();

                for (int i = 1; i <= userList.Count; i++)
                {
                    var emailAvailable = await _userManager.FindByEmailAsync(username);
                    if (username == "" || emailAvailable != null)
                    {
                        if (i == 1)
                        {
                            string name = ConvertTurkishToEnglish(addPersonelDTO.FirstName);
                            string nameToLower = name.ToLower();
                            string lastName = ConvertTurkishToEnglish(addPersonelDTO.LastName);
                            string lastNameToLower = lastName.ToLower();
                            username = ($"{nameToLower}.{lastNameToLower}{adminEmailDomain}").Replace(" ", "");
                        }
                        else
                        {
                            string name = ConvertTurkishToEnglish(addPersonelDTO.FirstName);
                            string nameToLower = name.ToLower();
                            string lastName = ConvertTurkishToEnglish(addPersonelDTO.LastName);
                            string lastNameToLower = lastName.ToLower();
                            username = ($"{nameToLower}.{lastNameToLower}{i}{adminEmailDomain}").Replace(" ", "");
                        }

                    }
                    else
                    {
                        break;
                    }
                }

                var user = new AppUser
				{
					Email = username,
					UserName = username,
					PhoneNumber = addPersonelDTO.Phone,
					EmailConfirmed = false,
				};
				await _userManager.CreateAsync(user, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
				AppUser createdUser = await _userManager.FindByNameAsync(username);
				IdentityRole role = await _roleManager.FindByNameAsync("User");
				await _userManager.AddToRoleAsync(createdUser, role.Name);

				string fileUrl = _photoService.UploadFile(addPersonelDTO.ProfilePicture, user.Id);
				string fileName = _photoService.GenerateFileName(addPersonelDTO.ProfilePicture.FileName, user.Id);

				var department = await _departmentService.TGetByWhereAsync(x => x.Name == addPersonelDTO.Department);
				var profession = await _professionService.TGetByWhereAsync(x => x.Name == addPersonelDTO.Profession);

				var employee = new Employee
				{
					FirstName = addPersonelDTO.FirstName.Trim(),
					LastName = addPersonelDTO.LastName.Trim(),
					MiddleName = addPersonelDTO.MiddleName,
					SecondLastName = addPersonelDTO.SecondLastName,
					FileName = fileName,
					FilePath = fileUrl,
					StartDate = addPersonelDTO.StartDate,
					ProfessionId = profession.Id,
					DepartmentId = department.Id,
					TRIdentityNumber = addPersonelDTO.TRIdentityNumber.Trim(),
					Salary = addPersonelDTO.Salary,
					Status = Domain.Enums.Status.Active,
					AppUserId = user.Id,
					PlaceOfBirth = addPersonelDTO.PlaceOfBirth,
					BirthDate = addPersonelDTO.BirthDate,
					Address = addPersonelDTO.Address.Trim(),
					City = addPersonelDTO.City.Trim(),
					County = addPersonelDTO.County.Trim(),
					CompanyId = admin.CompanyId
				};

				createdUser.Employee = employee;
				employee.AppUser = createdUser;


				await this.TAddAsync(employee);

				_emailService.SendPasswordToEmail(user.Email, "AAAaaa.123");



			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}


		}

		private string ConvertTurkishToEnglish(string input)
		{
			string[] turkishChars = { "ç", "ğ", "ı", "ö", "ş", "ü", "Ç", "Ğ", "İ", "Ö", "Ş", "Ü" };
			string[] englishChars = { "c", "g", "i", "o", "s", "u", "C", "G", "I", "O", "S", "U" };

			for (int i = 0; i < turkishChars.Length; i++)
			{
				input = Regex.Replace(input, turkishChars[i], englishChars[i]);
			}

			return input;
		}

		public async Task<bool> UpdatePasswordAsync(string userId, string oldPassword, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) return false;

			
			user.EmailConfirmed = true;
			var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

			return result.Succeeded;
		}

		public bool ArePasswordsMatching(string newPassword, string confirmPassword)
		{
			return newPassword == confirmPassword;
		}

		public async Task<GetAllProcessDTO> GetAllProcess(string appUserId)
		{
			try
			{
				var appUser = await _userManager.FindByIdAsync(appUserId);
				var admin = await this.TGetByWhereAsync(x => x.AppUserId == appUser.Id);


				var expenseList = await _expenseRepository.GetAllAsync(x=> x.Employee.CompanyId == admin.CompanyId);
				var leaveList = await _leaveRepository.GetAllAsync(x => x.Employee.CompanyId == admin.CompanyId);
				var advancePaymentList = await _advancePaymentRepository.GetAllAsync(x => x.Employee.CompanyId == admin.CompanyId);

				GetAllProcessDTO getAllProcessDTO = new GetAllProcessDTO();
				List<GetExpenseDTO> getExpenseDTOs = new List<GetExpenseDTO>();
				List<GetLeaveDTO> getLeaveDTOs = new List<GetLeaveDTO>();
				List<GetAdvancePaymentDTO> getAdvancePaymentDTOs = new List<GetAdvancePaymentDTO>();

				if (expenseList != null)
				{
					foreach (var expense in expenseList)
					{
						GetExpenseDTO getExpenseDTO = new GetExpenseDTO();

						var employee = await _employeeRepository.GetByWhereAsync(x=> x.Id == expense.EmployeeId);
						getExpenseDTO.FullName = employee.FirstName+" "+employee.LastName;
						getExpenseDTO.ApprovalStatus = Enum.GetName(typeof(ApprovalStatus), expense.ApprovalStatus);
						getExpenseDTO.EmployeeID = expense.EmployeeId;
						getExpenseDTO.ExpenseId = expense.Id;
						getExpenseDTO.Description = expense.Description;
						getExpenseDTO.Type = Enum.GetName(typeof(ExpenseType), expense.ExpenseType);
						getExpenseDTO.ProcessType = Domain.Enums.ProcessType.Expens.ToString();
						getExpenseDTO.ExpenseDate = expense.ExpenseDate;
						getExpenseDTO.Currency = Enum.GetName(typeof(Currency), expense.Currency);
						getExpenseDTO.Amount = expense.Amount;
						getExpenseDTO.InvoicePath = expense.InvoicePath;
						getExpenseDTOs.Add(getExpenseDTO);
					}
				}

                getAllProcessDTO.Expenses = getExpenseDTOs;

				if (leaveList != null)
				{
					foreach (var leave in leaveList)
					{
						GetLeaveDTO getLeaveDTO = new GetLeaveDTO();

						var employee = await _employeeRepository.GetByWhereAsync(x => x.Id == leave.EmployeeId);
						getLeaveDTO.FullName = employee.FirstName + " " + employee.LastName;
						getLeaveDTO.EmployeeID = leave.EmployeeId;
						getLeaveDTO.LeaveID = leave.Id;
						getLeaveDTO.Type = Enum.GetName(typeof(LeaveTypes), leave.Type);
						getLeaveDTO.Status = Enum.GetName(typeof(ApprovalStatus), leave.Status);
						getLeaveDTO.LeaveDate = leave.LeaveDate;
						getLeaveDTO.DueDate = leave.DueDate;
						getLeaveDTO.Description = leave.Description;
						getLeaveDTO.RequestDate = leave.RequestDate;
						getLeaveDTO.ProcessType = Domain.Enums.ProcessType.Leave.ToString();

						getLeaveDTOs.Add(getLeaveDTO);
					}
				}

				getAllProcessDTO.Leaves = getLeaveDTOs;

				if (advancePaymentList != null)
				{
					foreach (var advancePayment in advancePaymentList)
					{
						GetAdvancePaymentDTO getAdvancePaymentDTO = new GetAdvancePaymentDTO();

						var employee = await _employeeRepository.GetByWhereAsync(x => x.Id == advancePayment.EmployeeId);
						getAdvancePaymentDTO.FullName = employee.FirstName + " " + employee.LastName;
						getAdvancePaymentDTO.EmployeeID = advancePayment.EmployeeId;
						getAdvancePaymentDTO.AdvancePaymentId = advancePayment.AdvancePaymentId;
						getAdvancePaymentDTO.Description = advancePayment.Description;
						getAdvancePaymentDTO.Currency = Enum.GetName(typeof(Currency), advancePayment.Currency);
						getAdvancePaymentDTO.AdvanceType = Enum.GetName(typeof(AdvanceType), advancePayment.AdvanceType);
						getAdvancePaymentDTO.Amount = advancePayment.Amount;
						getAdvancePaymentDTO.ApprovalStatus = Enum.GetName(typeof(ApprovalStatus), advancePayment.ApprovalStatus);
						getAdvancePaymentDTO.ProcessType = Domain.Enums.ProcessType.AdvancePayment.ToString();

						getAdvancePaymentDTOs.Add(getAdvancePaymentDTO);
					}
				}

				getAllProcessDTO.AdvancePayments = getAdvancePaymentDTOs;

				return getAllProcessDTO;
			}
			catch (Exception)
			{

				throw new Exception("Bir hata meydana geldi!");
			}


		}

		public async Task<CompanyLogoDTO> GetCompanyLogo(string appUserId)
		{
			try
			{
				var employee = await _employeeRepository.GetByWhereAsync(x => x.AppUserId == appUserId);
				if (employee == null) { throw new Exception("Kullanıcı Bulunama!"); }

				var company = await _companyRepository.GetByWhereAsync(x => x.CompanyId == employee.CompanyId);
				if (company == null) { throw new Exception("Şirket Bulunama!"); }

				var componyLogoDto = new CompanyLogoDTO();
				componyLogoDto.CompanyLogo = company.LogoPath;

				return componyLogoDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			

		}
	}
}
