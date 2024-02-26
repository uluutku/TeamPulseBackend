using FluentValidation;
using HRAppBackend.Application.Dto.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.CreateCompanyValidations
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyDTO>
    {
        public CreateCompanyValidator()
        {
            RuleFor(companyAdmin => companyAdmin.CompanyLogo)
           .NotNull()
           .WithMessage("Şirket logosu gereklidir.");

            RuleFor(companyAdmin => companyAdmin.CompanyName)
                .NotEmpty()
                .WithMessage("Şirket adı gereklidir.")
                .Length(2, 100)
                .WithMessage("Şirket adı 2 ile 100 karakter arasında olmalıdır.");

            RuleFor(companyAdmin => companyAdmin.CompanyDescription)
                .NotEmpty()
                .WithMessage("Şirket açıklaması gereklidir.");

            RuleFor(companyAdmin => companyAdmin.SubscriptionEnd)
                .GreaterThan(DateTime.Now)
                .WithMessage("Abonelik bitiş tarihi gelecekte olmalıdır.");

            RuleFor(companyAdmin => companyAdmin.CompanyAddress)
                .NotEmpty()
                .WithMessage("Şirket adresi gereklidir.");

            RuleFor(companyAdmin => companyAdmin.CompanyPhone)
                .Matches(@"^\+?\d[\d -]{8,12}\d$")
                .WithMessage("Geçersiz telefon numarası formatı.");

            RuleFor(companyAdmin => companyAdmin.AdminEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Geçersiz e-posta adresi formatı.");

            RuleFor(companyAdmin => companyAdmin.AdminPassword)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Şifre en az 8 karakter uzunluğunda olmalıdır.");

            RuleFor(companyAdmin => companyAdmin.AdminTRIdentityNumber)
                .NotEmpty()
                .Length(11)
                .Matches(@"^\d{11}$")
                .WithMessage("Geçersiz TC Kimlik Numarası formatı.");

            RuleFor(companyAdmin => companyAdmin.AdminBirthDate)
                .LessThan(DateTime.Now)
                .WithMessage("Doğum tarihi geçmişte olmalıdır.");
        }
    }
}
