using FluentValidation;
using HRAppBackend.Application.Dto.AdminDTOs;
using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HRAppBackend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    [Authorize(AuthenticationSchemes = "User")]
	[Authorize(Roles = "User")]
	public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<ExpenseDTO> _validator;

        public ExpenseController(IExpenseService expenseService, IAuthenticationService authenticationService, IValidator<ExpenseDTO> validator)
        {
            _expenseService = expenseService;
            _authenticationService = authenticationService;
            _validator = validator;
        }

        [HttpPost("CreateExpense")]
        public async Task<IActionResult> CreateExpense([FromForm] ExpenseDTO expenseDto)
        {
            try
            {
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);

                await _expenseService.CreateExpense(appUserId, expenseDto);

                return Ok("Islem Basarili!");
            }
            catch (Exception)
            {
                return BadRequest("İşlem Başarısız!");
            }
        }

        [HttpPut("UpdateExpense")]
        public async Task<IActionResult> Update(int expenseId, [FromForm] UpdateExpanseDTO updateExpanseDTO)
        {
            try
            {
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);

                await _expenseService.UpdateExpense(appUserId, expenseId, updateExpanseDTO);

                return Ok("İşlem Başarılı!");
            }
            catch (Exception)
            {

                return BadRequest("İşlem Başarısız!");
            }
        }

        [HttpDelete("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            var token = tokenHeader.Split(" ")[1];
            var appUserId = await _authenticationService.GetUserIdFromToken(token);

            await _expenseService.DeleteExpense(expenseId);

            return Ok("İşlem Başarılı!");
        }

        [HttpGet("GetExpenses")]
        public async Task<IActionResult> GetExpenses()
        {
            try
            {
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);

                List<GetExpenseDTO> expenseList = await _expenseService.GetAllExpenses(appUserId);

                return Ok(expenseList);
            }
            catch (Exception)
            {
                return BadRequest("İşlem Başarısız!");
            }
        }
    }
}
