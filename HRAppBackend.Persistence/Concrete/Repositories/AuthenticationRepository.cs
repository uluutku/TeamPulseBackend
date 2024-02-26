using HRAppBackend.Application.Abstract.IRepositories;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Persistence.Concrete.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationRepository(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> UserExistAsync(string email)
        {
            //check the user
            var user =  await _userManager.FindByNameAsync(email);
            return user != null;
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                return result.Succeeded;
                // email confirmasyonu yapildiktan sonra: yukaridaki satir buna gore guncellenecek.
            }
            return false;
        }

    }
}
