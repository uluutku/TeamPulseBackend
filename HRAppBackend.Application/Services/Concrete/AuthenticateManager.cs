using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Application.Dto.TokenDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Concrete
{
    public class AuthenticateManager : IAuthenticationService
	{
		private readonly IAuthenticationRepository _repository;
		private readonly IEmployeeService _employeeService;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IEmailService _emailService;
		private readonly ICompanyService _companyService;

		public AuthenticateManager(IAuthenticationRepository repository, IEmployeeService employeeService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IEmailService emailService, ICompanyService companyService)
		{
			_repository = repository;
			_employeeService = employeeService;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_emailService = emailService;
			_companyService = companyService;
		}

		public async Task<TokenDTO> AuthenticateUserAsync(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentException("E-posta veya şifre boş olamaz");
			}

			if (!IsValidEmail(email))
			{
				throw new ArgumentException("Geçersiz e-posta formatı");
			}

			var user = await _userManager.FindByEmailAsync(email);
			var employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);

			var company = await _companyService.TGetByWhereAsync(x => x.CompanyId == employee.CompanyId);

			if(company.RemainingSubscriptionDays <= 0) 
			{
				throw new ArgumentException("Abonelik Süresini Yenileyiniz!");
			}

			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
				if (result.Succeeded)
				{
					return await _tokenService.CreateAccessToken(user);
				}
				// email confirmasyonu yapildiktan sonra: yukaridaki satir buna gore guncellenecek.
			}

			throw new ArgumentException("Email veya şifre yanlış");

		}

		public async Task<bool> CheckConfirmCode(int code, string email)
		{

			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				if (user.ConfirmCode == code)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				throw new Exception("Kullanıcı Bulunamadı!");
			}

			

		}

		public async Task<string> GetUserIdFromToken(Microsoft.Extensions.Primitives.StringValues token)
		{

			var appUserId = await _tokenService.ExtractEmailFromToken(token);

			return appUserId;
		}

		public async Task SendConfirmCodeToMail(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				Random random = new Random();
				user.ConfirmCode = random.Next(100000, 1000000);
				await _userManager.UpdateAsync(user);

				_emailService.SendCodeToEmail(user.ConfirmCode, email);

			}
		}

		public async Task<bool> UpdatePasswordAsync(string email, string newPassword, string confirmNewPassword)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user == null) { throw new Exception("Kullanıcı Bulunamadı!"); }

				

				var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
				await _userManager.UpdateAsync(user);
				return result.Succeeded;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			
		}

		private bool IsValidEmail(string email)
		{
			//bir email adresinin geçerli bir formata sahip olup olmadığını kontrol etmek için kullanılan bir regular expression (regex) ifadesi
			return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$");
		}


	}
}
