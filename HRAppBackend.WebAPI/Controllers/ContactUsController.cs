using HRAppBackend.Application.Dto.ContactUsDTOs;
using HRAppBackend.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRAppBackend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "User")]
    [Authorize(AuthenticationSchemes = "Admin")]
	[Authorize(Roles = "User,Admin")]
	
	public class ContactUsController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactUsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendmail")]
        public IActionResult SendMail([FromBody] ContactUsDTO contactUsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _emailService.SendContactUsMail(contactUsDTO);
                return Ok("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "E-posta gönderilirken bir hata oluştu.");
            }
        }
    }

}
