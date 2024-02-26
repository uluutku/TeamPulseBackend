using HRAppBackend.Application.Abstract;
using HRAppBackend.Application.Dto.TokenDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmployeeService _employeeService;
		private readonly UserManager<AppUser> _userManager;
		private readonly ICompanyService _companyService;


		public TokenService(IConfiguration configuration, RoleManager<IdentityRole> roleManager, IEmployeeService employeeService, UserManager<AppUser> userManager, ICompanyService companyService)
        {
            _configuration = configuration;
			_roleManager = roleManager;
			_employeeService = employeeService;
			_userManager = userManager;
            _companyService = companyService;
		}
        public  async Task<TokenDTO> CreateAccessToken(AppUser user)
        {
            TokenDTO token = new();
			var roles = await _userManager.GetRolesAsync(user);
			var currentRole = roles[0];
			Employee employee = await _employeeService.TGetByWhereAsync(x => x.AppUserId == user.Id);
            Company company = await _companyService.TGetByWhereAsync(x => x.CompanyId == employee.CompanyId);

			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddSeconds(Convert.ToInt32(_configuration["JwtSettings:AccessTokenExpirationMinutes"]));
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("Email", user.Email),
                new Claim("username", user.UserName),
                new Claim("filePath", employee.FilePath),
                new Claim("currentRole", currentRole),
                new Claim(ClaimTypes.Role, currentRole),
                new Claim("firstName", employee.FirstName),
                new Claim("lastName", employee.LastName),
                new Claim("companyName", company.CompanyName),
                new Claim("emailConfirmed", user.EmailConfirmed.ToString())
            };
            JwtSecurityToken securityToken = new(
                audience: _configuration["JwtSettings:Audience"],
                issuer: _configuration["JwtSettings:Issuer"],
                expires:token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );
            JwtSecurityTokenHandler securityTokenHandler = new();
            token.AccressToken = securityTokenHandler.WriteToken(securityToken);

            return token;
        }


		public async Task<string> ExtractEmailFromToken(Microsoft.Extensions.Primitives.StringValues token)
		{
			try
			{
				// Token'ı çözmek için gizli anahtar ve diğer konfigürasyonları belirtin
				var tokenHandler = new JwtSecurityTokenHandler();
				var claimsPrincipal = tokenHandler.ReadJwtToken(token);



				var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

				return userId;
			}
			catch (Exception ex)
			{
				throw new KeyNotFoundException("Token Çözümlemesi Başarısız Oldu!");
			}
		}

	}
}
