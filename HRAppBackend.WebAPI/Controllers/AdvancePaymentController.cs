using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRAppBackend.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]	
	[Authorize(AuthenticationSchemes = "User")]
	[Authorize(Roles ="User")]

	public class AdvancePaymentController : ControllerBase
	{
		private readonly IAdvancePaymentService _advancePaymentService;
		private readonly IAuthenticationService _authenticationService;

		public AdvancePaymentController(IAdvancePaymentService advancePaymentService, IAuthenticationService authenticationService)
		{
			_advancePaymentService = advancePaymentService;
			_authenticationService = authenticationService;
		}

		[HttpPost("CreateAdvancePayment")]
		public async Task<IActionResult> CreateAdvancePayment([FromBody] CreateAdvancePaymentDTO createAdvancePaymentDTO)
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				await _advancePaymentService.CreateAdvancePayment(appUserId, createAdvancePaymentDTO);

				return Ok("İşlem Başarılı!");
			}
			catch (Exception)
			{
				return BadRequest("İşlem Başarısız!");
			}
		}

		[HttpPut("UpdateAdvancePayment")]
		public async Task<IActionResult> UpdateAdvancePayment(int advanceId, [FromBody] UpdateAdvancePaymentDTO updateAdvancePaymentDTO)
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				await _advancePaymentService.UpdateAdvancePayment(appUserId, advanceId, updateAdvancePaymentDTO);

				return Ok("İşlem Başarılı!");
			}
			catch (Exception)
			{

				return BadRequest("İşlem Başarısız!");
			}

		}


		[HttpDelete("DeleteAdvancePayment")]
		public async Task<IActionResult> DeleteAdvancePayment(int advanceId)
		{
			string tokenHeader = HttpContext.Request.Headers["Authorization"];
			var token = tokenHeader.Split(" ")[1];
			var appUserId = await _authenticationService.GetUserIdFromToken(token);

			await _advancePaymentService.DeleteAdvancePayment(advanceId);

			return Ok("İşlem Başarılı!");
		}


		[HttpGet("GetAdvancePayments")]
		public async Task<IActionResult> GetAdvancePayments()
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				List<GetAdvancePaymentDTO> advanceList = await _advancePaymentService.GetAllAdvancePayments(appUserId);

				return Ok(advanceList);
			}
			catch (Exception)
			{
				return BadRequest("İşlem Başarısız!");
			}
		}
	}
}
