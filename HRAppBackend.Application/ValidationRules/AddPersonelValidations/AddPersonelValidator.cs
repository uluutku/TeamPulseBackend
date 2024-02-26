using FluentValidation;
using HRAppBackend.Application.Dto.AdminDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules.AddPersonelValidations
{
    public class AddPersonelValidator : AbstractValidator<AddPersonelDTO>
	{
		public AddPersonelValidator()
		{
			RuleFor(x => x.ProfilePicture).NotNull().WithMessage("Lütfen bir resim dosyası seçin.");

			RuleFor(dto => dto.Profession)
				.NotEmpty().WithMessage("Departman alanı boş geçilemez!");

			RuleFor(x => x.Profession)
				.NotEmpty().WithMessage("Meslek adı boş bırakılamaz!");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("Ad alanı boş bırakılamaz!")
				.MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır!")
				.MinimumLength(2).WithMessage("İsim En Az 3 Haneli Olmalı!");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Soyad alanı boş bırakılamaz!")
				.MaximumLength(50).WithMessage("Soyisim en fazla 50 karakter olmalıdır!")
				.MinimumLength(3).WithMessage("Soyisim Alanı En Az 3 Haneli Olmalı!");

			RuleFor(x => x.MiddleName).MaximumLength(50).WithMessage("Orta ad en fazla 50 karakter olmalıdır!");

			RuleFor(x => x.SecondLastName).MaximumLength(50).WithMessage("İkinci soyad en fazla 50 karakter olmalıdır!");

			RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi boş bırakılamaz.");

			RuleFor(x => x.TRIdentityNumber)
				.NotEmpty().WithMessage("Kimlik numarası boş bırakılamaz.")
				.Length(11).WithMessage("Kimlik numarası 11 karakter olmalıdır.");

			RuleFor(x => x.Salary)
				.NotEmpty().WithMessage("Maaş alanı boş bırakılamaz.")
				.GreaterThanOrEqualTo(17000).WithMessage("Maaş Asgari Ücretten Az Olamaz!");

			RuleFor(x => x.Address)
				.NotEmpty().WithMessage("Adres boş bırakılamaz.")
				.MaximumLength(200).WithMessage("Adres Tanımlaması En Fazla 200 Karakter Olmalı!")
				.MinimumLength(20).WithMessage("Adres Tanımlama En Az 20 Karakter Olmalı!");

			RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş bırakılamaz.");

			RuleFor(x => x.County).NotEmpty().WithMessage("İlçe boş bırakılamaz.");

			RuleFor(x => x.BirthDate)
				.NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.");

			RuleFor(x => x.PlaceOfBirth).NotEmpty().WithMessage("Doğum yeri boş bırakılamaz.");

			RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
			.Matches(@"^05[0-9]{9}$").WithMessage("Geçerli bir telefon numarası girin.");

		}
	}
}
