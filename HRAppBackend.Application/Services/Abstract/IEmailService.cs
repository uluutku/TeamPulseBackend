using HRAppBackend.Application.Dto;
using HRAppBackend.Application.Dto.ContactUsDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface IEmailService
    {
        public void SendCodeToEmail(int code,string email);
        public void SendPasswordToEmail(string mail, string password);
        public void SendContactUsMail(ContactUsDTO contactUsDTO);
    }
}
