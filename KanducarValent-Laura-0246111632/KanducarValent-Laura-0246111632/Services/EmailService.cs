using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rad.Model;


namespace KanducarValent_Laura_0246111632.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(Contact contactModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(contactModel.Name, contactModel.Email));
            message.To.Add(new MailboxAddress("Vlasnik", _config["EmailSettings:EmailOwner"]));
            message.Subject = "Nova poruka s kontakt forme";

            message.Body = new TextPart("plain")
            {
                Text = $"Korisnik Vam je poslao poruku!\n" +
                $"Ime korisnika: {contactModel.Name}" +
                $"\nMožete mu odgovoriti na email: {contactModel.Email}\n\n" +
                $"Upit korisnika:\n" +
                $"{contactModel.Message}"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]
                ), SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _config["EmailSettings:Username"],
                _config["EmailSettings:Password"]
                );

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}
