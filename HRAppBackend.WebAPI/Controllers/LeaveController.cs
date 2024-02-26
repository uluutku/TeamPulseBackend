using HRAppBackend.Application.Dto.LeaveDTO;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.ValidationRules.LeaveValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRAppBackend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "User")]
	[Authorize(Roles = "User")]
	public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly IAuthenticationService _authenticationService;

        public LeaveController(ILeaveService leaveService, IAuthenticationService authenticationService)
        {
            _leaveService = leaveService;
            _authenticationService = authenticationService;
        }

        [HttpPost("CreateLeave")]
        public async Task<IActionResult> CreateLeave([FromBody] LeaveDTO leaveDto)
        {           

            if (leaveDto == null)
            {
                return BadRequest("Izin bilgileri bos gecilemez.");
            }
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            var token = tokenHeader.Split(" ")[1];
            var appUserId = await _authenticationService.GetUserIdFromToken(token);
            if (appUserId == null)
            {
                return Unauthorized();
                throw new Exception("Kullanici girisiniz basarisiz oldu!");
            }
            else
            {
                await _leaveService.CreateLeave(appUserId, leaveDto);
                return Ok("Izin olusturma isleminiz basari ile tamamlandi.");
            }
        }

        // Izinleri gosterme islemi yapilacak
        [HttpGet("GetLeaves")]
        public async Task<IActionResult> GetLeaves()
        {           
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            var token = tokenHeader.Split(" ")[1];
            var appUserId = await _authenticationService.GetUserIdFromToken(token);
            if (appUserId == null)
            {
                return Unauthorized();
                throw new Exception("Kullanici girisiniz basarisiz oldu!");
            }
            else
            {
                var leaves = await _leaveService.GetLeavesByUserId(appUserId);
                return Ok(leaves);
            }
        }


        //Izin guncelleme islemi yapilacak
        [HttpPut("UpdateLeave")]
        public async Task<IActionResult> UpdateLeave(int leaveId, [FromBody] LeaveUpdateDTO updateLeaveDTO)
        {
            if (updateLeaveDTO == null)
            {
                return BadRequest("Guncellenecek izin bilgileri bos gecilemez.");
            }
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            var token = tokenHeader.Split(" ")[1];
            var appUserId = await _authenticationService.GetUserIdFromToken(token);
            if (appUserId == null)
            {
                return Unauthorized();
                throw new Exception("Kullanici girisiniz basarisiz oldu!");
            }
            else
            {
                await _leaveService.Update(leaveId,appUserId, updateLeaveDTO);
                return Ok("Izin guncelleme isleminiz basari ile tamamlandi.");
            }
        }
        // Izin silme islemi yapilacak
        [HttpDelete("DeleteLeave")]
        public async Task<IActionResult> DeleteLeave(int leaveId)
        {

            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            var token = tokenHeader.Split(" ")[1];
            var appUserId = await _authenticationService.GetUserIdFromToken(token);
            if (appUserId == null)
            {
                return Unauthorized();
                throw new Exception("Kullanici girisiniz basarisiz oldu!");
            }
            else
            {
                try
                {
                    await _leaveService.Delete(appUserId, leaveId);
                    return Ok("Izin silme isleminiz basari ile tamamlandi.");
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

    }
}
