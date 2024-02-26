using FluentValidation;
using HRAppBackend.Application.Dto.AdminDTOs;
using HRAppBackend.Application.Dto.ExpenseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.ExpenseValidations
{
    public class CreateExpenseValidator : AbstractValidator<ExpenseDTO>
    {
        public CreateExpenseValidator()
        {
            RuleFor(x => x.Type).NotNull().WithMessage("Lutfen harcama tipini seciniz.");

            RuleFor(x => x.Currency).NotNull().WithMessage("Lutfen harcama kur tipini seciniz.");

            RuleFor(x => x.InvoicePath).NotNull().WithMessage("Lutfen PDF formatinda bir harcama faturasi yukleyiniz.");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Lutfen harcama tutarini giriniz.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Lutfen harcamaya iliskin aciklama giriniz.");
        }
    }
}
