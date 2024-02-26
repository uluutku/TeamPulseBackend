using HRAppBackend.Application.Dto.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> AuthenticateUserAsync(string email, string password);
		
		Task<string> GetUserIdFromToken(Microsoft.Extensions.Primitives.StringValues token);

		Task<bool> CheckConfirmCode(int code, string email);

		Task SendConfirmCodeToMail(string email);

		Task<bool> UpdatePasswordAsync(string email, string newPassword, string confirmNewPassword);
	}
}
