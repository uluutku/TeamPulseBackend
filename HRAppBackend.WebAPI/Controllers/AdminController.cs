using FluentValidation;
using HRAppBackend.Application.Dto.AdminDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.ValidationRules.AddPersonelValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace HRAppBackend.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = "Admin")]
	[Authorize(Roles = "Admin")]

	public class AdminController : Controller
	{
		private readonly IEmployeeService _employeeService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IValidator<AddPersonelDTO> _validator;
		private readonly IDepartmentService _departmentService;
		private readonly IProfessionService _professionService;
		private readonly IExpenseService _expenseService;
		private readonly IAdvancePaymentService _advancePaymentService;
		private readonly ILeaveService _leaveService;

		public AdminController(IEmployeeService employeeService, IAuthenticationService authenticationService, IValidator<AddPersonelDTO> validator,
			IDepartmentService departmentService, IProfessionService professionService, IExpenseService expenseService, IAdvancePaymentService advancePaymentService, ILeaveService leaveService)
		{
			_employeeService = employeeService;
			_authenticationService = authenticationService;
			_validator = validator;
			_departmentService = departmentService;
			_professionService = professionService;
			_expenseService = expenseService;
			_advancePaymentService = advancePaymentService;
			_leaveService = leaveService;
		}

		[HttpGet("GetAllEmployees")]
		public async Task<IActionResult> GetAllEmployees()
		{
			try

			{
				try
				{
					string tokenHeader = HttpContext.Request.Headers["Authorization"];
					var token = tokenHeader.Split(" ")[1];
					var appUserId = await _authenticationService.GetUserIdFromToken(token);
					var employees = await _employeeService.GetAllEmployeesAsync(appUserId);


					return Ok(employees);
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

		[HttpGet("GetDepartmentsAndProfessions")]
		public async Task<IActionResult> GetDepartmentsAndEProfessionsList()
		{

			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);
				ProfessionDepartmantListDTO professionDepartmantListDTO = new ProfessionDepartmantListDTO();
				var departments = await _departmentService.TGetAllAsync();
				professionDepartmantListDTO.Departments = departments.Select(d => d.Name).ToList();
				var professions = await _professionService.TGetAllAsync();
				professionDepartmantListDTO.Professions = professions.Select(d => d.Name).ToList();

				return Ok(professionDepartmantListDTO);
			}
			catch (Exception)
			{
				return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}

		}

		[HttpPost("AddEmployee")]
		public async Task<IActionResult> AddEmploye([FromForm] AddPersonelDTO addPersonelDTO)
		{

			var validationResult = _validator.Validate(addPersonelDTO);

			if (!validationResult.IsValid)
			{

				var firstError = validationResult.Errors.FirstOrDefault();
				return BadRequest(firstError?.ErrorMessage);
			}


			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				await _employeeService.AddPersonel(appUserId, addPersonelDTO);

				return Ok("Kullanıcı Kaydı Başarılı!");
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
				return StatusCode(500, ex.Message);
			}

		}

		[HttpGet("GetUserUpdateDetails")]
		public async Task<IActionResult> GetUserUpdateDetails(string appUserId)
		{
			try
			{
				var userDetails = await _employeeService.GetUserUpdateDetailsAsync(appUserId);
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

		[HttpPut("UpdateEmployee")]
		public async Task<IActionResult> UpdateEmployee(string id, [FromForm] UpdateDTO updatedto)
		{

			try
			{

				try
				{

					var fileUrl = await _employeeService.UpdateWithID(id, updatedto);

					if (!fileUrl.IsNullOrEmpty())
					{
						return Ok("Güncelleme Başarılı!");
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
		public async Task<IActionResult> GetUserDetails(string appUserId)
		{
			try
			{
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

		[HttpGet("GetAllProcess")]
		public async Task<IActionResult> GetAllProcess()
		{

			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				GetAllProcessDTO processList = await _employeeService.GetAllProcess(appUserId);

				return Ok(processList);
			}
			catch (Exception)
			{
				return BadRequest("Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}

		}

		[HttpPut("ProcessAcceptReject")]
		public async Task<IActionResult> ProcessAcceptReject(ProcessAcceptRejectDTO processAcceptRejectDTO)
		{
			try
			{
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				if (processAcceptRejectDTO.ProcessType == Domain.Enums.ProcessType.Expens.ToString())
				{
					await _expenseService.DecisionForProcess(processAcceptRejectDTO.ProcessId, processAcceptRejectDTO.ProcessResult);
				}
				else if (processAcceptRejectDTO.ProcessType == Domain.Enums.ProcessType.AdvancePayment.ToString())
				{
					await _advancePaymentService.DecisionForProcess(processAcceptRejectDTO.ProcessId, processAcceptRejectDTO.ProcessResult);
				}
				else if (processAcceptRejectDTO.ProcessType == Domain.Enums.ProcessType.Leave.ToString())
				{
					await _leaveService.DecisionForProcess(processAcceptRejectDTO.ProcessId, processAcceptRejectDTO.ProcessResult);
				}
				else
				{
					return BadRequest("İşlem Tipi Hatalı!");
				}


				return Ok("İşlem Başarılı!");
			}
			catch (Exception)
			{
				return BadRequest("İşlem Başarısız!");
			}
			
		}

	}
}
