using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Application.Services.Concrete;
using HRAppBackend.Infrastructure.Services.Email;
using HRAppBackend.Infrastructure.Services.Photo;
using HRAppBackend.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Infrastructure
{
    public static class ServiceRegistration
	{
		public static void RepositoriesInjections(this IServiceCollection services)
		{
			services.AddTransient<IPhotoService, PhotoService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IEmailService, EmailService>();
			
		}
	}
}
