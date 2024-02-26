using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HRAppBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	[Authorize(AuthenticationSchemes = "User")]
	[Authorize(AuthenticationSchemes = "SiteAdmin")]
	[Authorize(Roles = "User,Admin,SiteAdmin")]
	
	public class EmployeeController : ControllerBase
	{
		
		private readonly IAuthenticationService _authenticationService;
		private readonly IEmployeeService _employeeService;

		public EmployeeController( IAuthenticationService authenticationService, IEmployeeService employeeService)
        {
			
			_authenticationService = authenticationService;
			_employeeService = employeeService;
		}


		[HttpPost("UpdatePassword")]
		public async Task<IActionResult> UpdatePassword([FromBody] PasswordRenewalDTO passwordRenewalDTO)
		{
			string tokenHeader = HttpContext.Request.Headers["Authorization"];
			var token = tokenHeader.Split(" ")[1];
			var userId = await _authenticationService.GetUserIdFromToken(token);

			if (!_employeeService.ArePasswordsMatching(passwordRenewalDTO.NewPassword, passwordRenewalDTO.ConfirmPassword))
			{
				return BadRequest("New passwords do not match.");
			}

			var result = await _employeeService.UpdatePasswordAsync(userId, passwordRenewalDTO.OldPassword, passwordRenewalDTO.NewPassword);
			if (!result)
			{
				return BadRequest("Password update failed.");
			}

			return Ok("Password updated successfully.");
		}


		

		[HttpGet("GetSummary")]
		public async Task<IActionResult> GetSummary()
		{
			try
			{
				try
				{
					string tokenHeader = HttpContext.Request.Headers["Authorization"];
					var token = tokenHeader.Split(" ")[1];
					var appUserId = await _authenticationService.GetUserIdFromToken(token);
					var summary = await _employeeService.GetSummary(appUserId);
					return Ok(summary);
				}
				catch (Exception)
				{

					return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
				}



			}
			catch (ArgumentException ex)
			{
				return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
		}

		[HttpGet("GetUpdateDetails")]
		public async Task<IActionResult> GetUserUpdateDetailsAsync()
		{
			try
			{
				try
				{
					string tokenHeader = HttpContext.Request.Headers["Authorization"];
					var token = tokenHeader.Split(" ")[1];
					var appUserId = await _authenticationService.GetUserIdFromToken(token);
					var summary = await _employeeService.GetUserUpdateDetailsAsync(appUserId);
					return Ok(summary);
				}
				catch (Exception)
				{

					return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
				}



			}
			catch (ArgumentException ex)
			{
				return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}

		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromForm] UpdateDTO updatedto)
		{
			try
			{

				try
				{
					string tokenHeader = HttpContext.Request.Headers["Authorization"];
					var token = tokenHeader.Split(" ")[1];
					var appUserId = await _authenticationService.GetUserIdFromToken(token);

					var fileUrl = await _employeeService.Update(appUserId, updatedto);

					if (!fileUrl.IsNullOrEmpty())
					{
						return Ok(fileUrl);
					}
					else
					{
						return BadRequest("Güncelleme başarısız. Lütfen tekrar deneyin.");
					}
				}
				catch (Exception)
				{

					return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
				}


			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
		}

		[HttpGet("GetDetails")]
		public async Task<IActionResult> GetUserDetails()
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				var userDetails = await _employeeService.GetUserDetailsAsync(appUserId);
				if (userDetails == null)
				{
					return NotFound("Hatali giris yaptiniz, tekrar deneyiniz!");
				}

				return Ok(userDetails);
			}
			catch (Exception)
			{

				return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}


		}


		[HttpGet("GetCompanyLogo")]
		public async Task<IActionResult> GetCompanyLogo()
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				var companyLogoDTO = await _employeeService.GetCompanyLogo(appUserId);

				return Ok(companyLogoDTO.CompanyLogo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

	}
}
