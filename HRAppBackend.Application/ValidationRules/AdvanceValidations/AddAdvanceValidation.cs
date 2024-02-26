using FluentValidation;
using HRAppBackend.Application.Dto.AdvancePaymentDTOs;
using HRAppBackend.Application.Dto.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.AdvanceValidations
{
	public class AddAdvanceValidation : AbstractValidator<CreateAdvancePaymentDTO>
	{
        public AddAdvanceValidation()
        {
			RuleFor(dto => dto.Description)
				.NotEmpty().WithMessage("Şifre Tekrar Alanı Boş Geçilemez!")
				.MinimumLength(15).WithMessage("Açıklama En Az 15 karakterden oluşmalıdır!")
				.MaximumLength(255).WithMessage("Açıklama En Az 255 karakterden oluşmalıdır!");

			RuleFor(dto => dto.Amount)
				.NotEmpty().WithMessage("Şifre Tekrar Alanı Boş Geçilemez!");

			RuleFor(dto => dto.Currency)
				.NotEmpty().WithMessage("Şifre Tekrar Alanı Boş Geçilemez!");

			RuleFor(dto => dto.Type)
				.NotEmpty().WithMessage("Şifre Tekrar Alanı Boş Geçilemez!");

		}
    }
}
