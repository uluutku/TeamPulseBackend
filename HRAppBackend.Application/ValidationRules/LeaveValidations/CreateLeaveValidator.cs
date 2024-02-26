using FluentValidation;
using HRAppBackend.Application.Dto.LeaveDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.LeaveValidations
{
    public class CreateLeaveValidator : AbstractValidator<LeaveDTO>
    {
        public CreateLeaveValidator()
        {
            RuleFor(x => x.Type).NotNull().WithMessage("Lutfen izin tipini seciniz.");
           

            RuleFor(x => x.LeaveDate).NotEmpty().WithMessage("Lutfen izin baslangic tarihini giriniz.");

            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Lutfen izin bitis tarihini giriniz.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Lutfen izin aciklamasi giriniz.");

        }
    }
}
