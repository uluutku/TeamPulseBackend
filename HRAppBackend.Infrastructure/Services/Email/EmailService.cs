using HRAppBackend.Application.Dto;
using HRAppBackend.Application.Dto.ContactUsDTOs;
using HRAppBackend.Application.Services.Abstract;
using HRAppBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace HRAppBackend.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {

        private readonly UserManager<AppUser> _userManager;

        public EmailService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public void SendPasswordToEmail(string mail, string password)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Team Pulse Project Admin", "pteampulse@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mail);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = @"<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Team Pulse Proje Yönetimi</title>
    <style>
        body {
            font-family: 'Century Gothic', 'Roboto', sans-serif;
            color: #333;
            margin: 0;
            padding: 0;
            background-color: #f8f8f8;
        }
        .container {
            max-width: 400px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 30px;
            border: 1px solid #e0e0e0;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        .code {
            font-size: 24px;
            padding: 15px;
            border: 2px solid #3498db;
            display: inline-block;
            background-color: #ffffff;
            margin-bottom: 30px;
            color: #3498db;
            border-radius: 8px;
            letter-spacing: 2px;
        }
        .message {
            margin-top: 20px;
            font-size: 18px;
        }
        img {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            margin-bottom: 20px;
        }
        h2 {
            color: #3498db;
        }
        p {
            font-size: 16px;
            line-height: 1.6;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <img src='https://i.ibb.co/G9mmRQd/1a08f6f0-c084-4b5c-b05f-94b2dfe404be-removebg-preview-copy.png' alt='Team Pulse Logo'>
        <div>
            <h2>Team Pulse Proje Şifreniz</h2>
            <p>Merhaba,</p>
            <p>Team Pulse projesine giriş için kullanılacak şifreniz:</p>
            <div class='code'>" + password + @"</div>
            <p class='message'>İyi günler dileriz.</p>
        </div>
    </div>
</body>
</html>";


            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Team Pulse Proje Giriş Şifresi";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("pteampulse@gmail.com", "mjzq upgk hbet fwuf");
            client.Send(mimeMessage);
            client.Disconnect(true);

        }

        public void SendCodeToEmail(int code, string email)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Team Pulse Project Admin", "pteampulse@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);

                mimeMessage.From.Add(mailboxAddressFrom);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = @"<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Team Pulse Proje Onay Kodu</title>
    <style>
        body {
            font-family: 'Century Gothic', 'Roboto', sans-serif;
            color: #333;
            margin: 0;
            padding: 0;
            background-color: #f8f8f8;
        }
        .container {
            max-width: 400px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 30px;
            border: 1px solid #e0e0e0;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        .code {
            font-size: 24px;
            padding: 15px;
            border: 2px solid #3498db;
            display: inline-block;
            background-color: #ffffff;
            margin-bottom: 30px;
            color: #3498db;
            border-radius: 8px;
            letter-spacing: 2px;
        }
        .message {
            margin-top: 20px;
            font-size: 18px;
        }
        img {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            margin-bottom: 20px;
        }
        h2 {
            color: #3498db;
        }
        p {
            font-size: 16px;
            line-height: 1.6;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <img src='https://i.ibb.co/G9mmRQd/1a08f6f0-c084-4b5c-b05f-94b2dfe404be-removebg-preview-copy.png' alt='Team Pulse Logo'>
        <div>
            <h2>Team Pulse Proje Onay Kodu</h2>
            <p>Merhaba,</p>
            <p>Team Pulse projesi için onay kodunuz:</p>
            <div class='code'>" + code + @"</div>
            <p class='message'>İyi çalışmalar dileriz.</p>
        </div>
    </div>
</body>
</html>";


                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = "Team Pulse Proje Giriş Şifresi";

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("pteampulse@gmail.com", "mjzq upgk hbet fwuf");
                client.Send(mimeMessage);
                client.Disconnect(true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void SendContactUsMail(ContactUsDTO contactUsDTO)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Team Pulse Project Admin", "pteampulse@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", "teampulse14@gmail.com");

                mimeMessage.From.Add(mailboxAddressFrom);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = $@"<!DOCTYPE html>
<html lang='tr'>
<head>
    <title>Konu: {contactUsDTO.Subject}</title>
</head>
<body>
    <div class='container'>
        <div>
            <h2>{contactUsDTO.Subject}</h2>
            <p>Merhaba,</p>
            <p>{contactUsDTO.Description}</p>
        </div>
    </div>
</body>
</html>";

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = contactUsDTO.Subject;

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("pteampulse@gmail.com", "mjzq upgk hbet fwuf");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
