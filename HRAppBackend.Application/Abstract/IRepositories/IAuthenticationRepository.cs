using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Abstract.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<bool> UserExistAsync(string email);
        Task<bool> AuthenticateUserAsync(string email, string password);
    }
}
