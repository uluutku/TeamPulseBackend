using FluentValidation;
using HRAppBackend.Application.Dto.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.CompanyValidations
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyDTO>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(company => company.CompanyName)
           .NotEmpty().WithMessage("Şirket adı boş olamaz.")
           .Length(2, 150).WithMessage("Şirket adı 2 ile 150 karakter arasında olmalıdır.");

            RuleFor(company => company.CompanyDescription)
                .NotEmpty().WithMessage("Şirket açıklaması boş olamaz.")
                .Length(5, 1000).WithMessage("Şirket açıklaması 5 ile 1000 karakter arasında olmalıdır.");

            RuleFor(company => company.SubscriptionEndDate)
                .NotEmpty().WithMessage("Abonelik bitiş tarihi boş olamaz.")
                .GreaterThan(DateTime.Today).WithMessage("Abonelik bitiş tarihi bugünden büyük olmalıdır.");

            RuleFor(company => company.CompanyAddress)
                .NotEmpty().WithMessage("Şirket adresi boş olamaz.")
                .Length(10, 500).WithMessage("Şirket adresi 10 ile 500 karakter arasında olmalıdır.");

            RuleFor(company => company.CompanyPhone)
                .NotEmpty().WithMessage("Şirket telefonu boş olamaz.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçersiz telefon numarası formatı.");

            RuleFor(company => company.CompanyLogo)
            .Must(file => file == null || (file.Length > 0 && file.Length <= 2 * 1024 * 1024)) // 2MB maksimum boyut
            .WithMessage("Logo dosyası boş olamaz ve maksimum 2MB boyutunda olmalıdır.")
            .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Sadece JPG, JPEG ve PNG dosya tiplerine izin verilir.")
            .When(company => company.CompanyLogo != null);
        }
    }
}
