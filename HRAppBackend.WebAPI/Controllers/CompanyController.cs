using HRAppBackend.Application.Dto.CompanyDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using HRAppBackend.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRAppBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize(AuthenticationSchemes = "SiteAdmin")]
	[Authorize(Roles = "SiteAdmin")]

	public class CompanyController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICompanyService _companyService;

        public CompanyController(IAuthenticationService authenticationService, ICompanyService companyService)
        {
            _authenticationService = authenticationService;
            _companyService = companyService;
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromForm] CreateCompanyDTO createCompanyDTO)
        {
            try
            {
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);

                var result = await _companyService.CreateCompany(createCompanyDTO);

                if (result)
                    return Ok("İşlem Başarılı");
                else
                    return BadRequest("Bir Hata Meydana Geldi");
            }
            catch (Exception)
            {

                return BadRequest("İşlem Başarısız!");
            }

        }

        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            try
            {
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);

                await _companyService.DeleteCompany(companyId);

                return Ok("Islem Basarili!");
            }
            catch (Exception)
            {
                return BadRequest("Islem Basarisiz!");
            }
            
        }

        [HttpGet("GetCompanys")]
        public async Task<IActionResult> GetExpenses()
        {
            try
            {
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				List<GetCompanysDTO> companyList = await _companyService.GetAllCompanys();

                return Ok(companyList);
            }
            catch (Exception)
            {
                return BadRequest("İşlem Başarısız!");
            }
        }

        [HttpPut("StopSubscription")]
        public async Task<IActionResult> StopSubscription(int companyId)
        {
            try
            {
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

                await _companyService.StopSubscription(companyId);

				return Ok("İşlem Başarılı!");
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            
        }

        [HttpPut("ExtendSubscription")]
        public async Task<IActionResult> ExtendSubscription([FromBody] SubscriptionDTO subscriptionDTO)
        {
            try
            {
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				await _companyService.ExtendSubscription(subscriptionDTO);

				return Ok("İşlem Başarılı!");
			}
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
			}
			
        }

        [HttpGet("GetCompanyUpdateDetails")]
        public async Task<IActionResult> GetCompanyUpdateDetails(int companyId)
        {
            try
            {
				string tokenHeader = HttpContext.Request.Headers["Authorization"];
				var token = tokenHeader.Split(" ")[1];
				var appUserId = await _authenticationService.GetUserIdFromToken(token);

				GetCompanyUpdateDetailsDTO companyDetails = await _companyService.GetCompanyUpdateDetails(companyId);

				return Ok(companyDetails);
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
			
		}
        

		[HttpPut("UpdateCompany")]
		public async Task<IActionResult> UpdateCompany(int companyId, [FromForm] UpdateCompanyDTO updateCompanyDTO)
		{
            try
			{
                string tokenHeader = HttpContext.Request.Headers["Authorization"];
                var token = tokenHeader.Split(" ")[1];
                var appUserId = await _authenticationService.GetUserIdFromToken(token);
                var result = await _companyService.UpdateCompany(companyId, updateCompanyDTO);

                if (result) return Ok("İşlem Başarılı");
                else return BadRequest("Bir Hata Meydana Geldi");
            }
            catch (Exception)
			{
                return BadRequest("İşlem Başarısız!");
            }
            
        }

	}
}
