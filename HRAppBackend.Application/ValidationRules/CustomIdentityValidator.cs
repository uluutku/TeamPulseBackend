using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.ValidationRules
{
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "SifreCokKisa",
                Description = $"Şifre en az {length} karakter olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "SifreBuyukHarfGerekli",
                Description = "Lütfen en az bir büyük harf girin"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "SifreKucukHarfGerekli",
                Description = "Lütfen en az bir küçük harf girin"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "SifreRakamGerekli",
                Description = "Lütfen en az bir rakam girin"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "SifreOzelKarakterGerekli",
                Description = "Lütfen en az bir sembol girin"
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = "GeçersizEposta",
                Description = "Lütfen geçerli bir e-posta adresi girin"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "AyniEpostaVar",
                Description = "Bu e-posta ile kayıtlı bir kullanıcı zaten var, lütfen e-postanızı değiştirin"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = "SifreUyuşmuyor",
                Description = "Girilen şifre yanlış"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = "SifreFarkliKarakterGerekli",
                Description = $"Şifre en az {uniqueChars} farklı karakter içermelidir"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = "GecersizKullaniciAdi",
                Description = "Kullanıcı adındaki karakterler geçersiz"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "AyniKullaniciAdiVar",
                Description = "Bu kullanıcı adı zaten alınmış, lütfen farklı bir tane seçin"
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = "VarsayilanHata",
                Description = "Kayıt sırasında bir hata oluştu"
            };
        }

    }
}
