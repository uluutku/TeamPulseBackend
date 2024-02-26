using Azure.Storage.Blobs.Models;
using HRAppBackend.Application.Dto.AccountDTOs;
using HRAppBackend.Application.Dto.TokenDTOs;
using HRAppBackend.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRAppBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		
		private readonly IEmailService _emailService;
		private readonly IAuthenticationService _authenticationService;

		public AccountController(IEmailService emailService, IAuthenticationService authenticationService)
		{
			_emailService = emailService;
			_authenticationService = authenticationService;
		}

		[HttpPost("SendCode")]
		public async Task<IActionResult> SendCodeToMail(EmailDTO emailDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			

			try
			{
				await _authenticationService.SendConfirmCodeToMail(emailDTO.Email);
				return Ok("Başarılı");
			}
			catch (Exception)
			{

				return BadRequest("Kod Gönderimi Başarısız Oldu!");
			}

			
			
		}

		[HttpPost("ConfirmCode")]
		public async Task<IActionResult> ConfirmCode(ConfirmCodeDTO confirmCodeDTO)
		{


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			int code = Convert.ToInt32(confirmCodeDTO.SCode);


			try
			{
				
				var result = await _authenticationService.CheckConfirmCode(code, confirmCodeDTO.Email);
				return Ok(result);
			}
			catch (Exception)
			{

				return BadRequest("Kod Kontrolü Başarısız Oldu");
			}

		}


		[HttpPut("ChangePassword")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
		{
			if(changePasswordDTO.Password != changePasswordDTO.ConfirmPassword)
			{
				return BadRequest("Şifreler Uyuşmuyor!");
			}

			try
			{
				var result = await _authenticationService.UpdatePasswordAsync(changePasswordDTO.Email,changePasswordDTO.Password,changePasswordDTO.ConfirmPassword);
				return Ok();
			}
			catch (Exception)
			{

				throw;
			}



			
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Geçersiz giriş bilgileri");
			}

			try
			{
				// Kullanıcıyı doğrula
				TokenDTO result = await _authenticationService.AuthenticateUserAsync(loginDTO.Email, loginDTO.Password);

				return Ok(result.AccressToken);

			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
		}

	}
}
