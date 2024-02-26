using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Dto.ContactUsDTOs
{
    public class ContactUsDTO
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(30, ErrorMessage = "Ad 30 karakterden uzun olmamalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu alanı zorunludur.")]
        [StringLength(255, ErrorMessage = "Konu 255 karakterden uzun olmamalıdır.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        public string Description { get; set; }
    }
}
