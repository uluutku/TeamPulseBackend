using FluentValidation;
using HRAppBackend.Application.Dto.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.AppUserValidations
{
    public class AppUserLoginValidator : AbstractValidator<LoginDTO>
    {
        public AppUserLoginValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("E-posta alanı boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Şifre alanı boş geçilemez.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter içermelidir.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

        }
    }
}
