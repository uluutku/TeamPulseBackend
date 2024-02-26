using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence;
using HRAppBackend.Persistence.Context;
using HRAppBackend.Persistence.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HRAppBackend.Application.ValidationRules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using HRAppBackend.Application.ValidationRules.AddPersonelValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using HRAppBackend.Application.ValidationRules.ForgotPasswordValidations;
using HRAppBackend.Application.ValidationRules.ChangePasswordValidations;
using HRAppBackend.Application.ValidationRules.AdvanceValidations;
using HRAppBackend.Application.ValidationRules.ExpenseValidations;
using HRAppBackend.Application.ValidationRules.LeaveValidations;
using HRAppBackend.Application.ValidationRules.CreateCompanyValidations;

namespace HRAppBackend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);           

           


            //Bearer
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });

                // Bearer token için güvenlik konfigürasyonunu ekleme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });

            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddPersonelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ForgotPasswordValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ChangePasswordValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddAdvanceValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateAdvanceValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateExpenseValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateLeaveValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCompanyValidator>());




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Corse
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://65aa1d990bb9735de29e06cf--harmonious-blini-64bee1.netlify.app")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod());
            });

            //Context
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conStr")));

            //IDENTITY
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddErrorDescriber<CustomIdentityValidator>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider)
                .AddRoles<IdentityRole>();

			

			builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            //Dependencies
            Persistence.ServiceRegistration.RepositoriesInjections(builder.Services);
            Application.ServiceRegistration.RepositoriesInjections(builder.Services);
            Infrastructure.ServiceRegistration.RepositoriesInjections(builder.Services);

			

			//Token Authorize
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer("Admin", options =>
             {
                 options.TokenValidationParameters = new()
                 {
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidAudience = builder.Configuration["JwtSettings:Audience"],
                     ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
                     LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                     NameClaimType = ClaimTypes.Name
                 };
             }).AddJwtBearer("User", options =>
			 {
				 options.TokenValidationParameters = new()
				 {
					 ValidateAudience = true,
					 ValidateIssuer = true,
					 ValidateLifetime = true,
					 ValidateIssuerSigningKey = true,
					 ValidAudience = builder.Configuration["JwtSettings:Audience"],
					 ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
					 LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
					 NameClaimType = ClaimTypes.Name
				 };
			 }).AddJwtBearer("SiteAdmin", options =>
			 {
				 options.TokenValidationParameters = new()
				 {
					 ValidateAudience = true,
					 ValidateIssuer = true,
					 ValidateLifetime = true,
					 ValidateIssuerSigningKey = true,
					 ValidAudience = builder.Configuration["JwtSettings:Audience"],
					 ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
					 LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
					 NameClaimType = ClaimTypes.Name
				 };
			 });

            


            var app = builder.Build();

            var serviceScope = app.Services.CreateScope();



            //Seed Data
            AppDbContext _context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            AdminSeedData.SeedData(_context, userManager, roleManager);



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}