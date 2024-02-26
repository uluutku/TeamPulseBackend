using FluentValidation;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.ChangePasswordValidations
{
    public class ChangePasswordValidator : AbstractValidator<PasswordRenewalDTO>
	{
        public ChangePasswordValidator()
        {
			RuleFor(dto => dto.NewPassword)
				.NotEmpty().WithMessage("Şifre alanı boş geçilemez.")
				.MinimumLength(8).WithMessage("Şifre en az 8 karakter içermelidir.")
				.Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
				.Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
				.Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
				.Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

			RuleFor(dto => dto.ConfirmPassword)
				.NotEmpty().WithMessage("Şifre Tekrar Alanı Boş Geçilemez!")
				.Equal(dto => dto.NewPassword).WithMessage("Şifreler Eşleşmiyor!");


			RuleFor(dto => dto.OldPassword)
				.NotEmpty().WithMessage("Şifre alanı boş geçilemez.")
				.MinimumLength(8).WithMessage("Şifre en az 8 karakter içermelidir.")
				.Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
				.Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
				.Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
				.Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

		}
    }
}
