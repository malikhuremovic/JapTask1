using JAPManagementSystem.DTOs.StudentDto;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace JAPManagementSystem.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendConfirmationEmail(AddStudentDto student)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailHostUserName").Value));
                email.To.Add(MailboxAddress.Parse(student.Email));
                email.Subject = "JAP Program";
                email.Body = new TextPart(TextFormat.Html) { Text = "<div style=\"text-align: center;\"><img style=\"width: 60%; height: auto;\" src=\"https://api.itkarijera.ba/api/cms/documentfile/download/2453\" /></div><div style=\"text-align:center; border-radius: 5px; font-family:sans-serif; color:#fff; font-weight: 700; padding: 8px; background-color:#689ed1;\"><h1>Welcome to Mistral!</h1></div><br/>" +
                    "<div style=\"width: 100%\"><h4>Hello " + student.FirstName + ", congratulations on becoming a Mistralovac! :)</h4><p style=\"color:#666666\">We are pleased to announce that we you have been accepted as a Junior Software Developer in our Junior Accelerator Pogram. We can not wait to have you onboard! <br/><br/>Thank you for choosing Mistral! <br/>Our colleague will contact you for further details.<br/><br/>All the best,<br/>Malik Huremović</p></div>"
                };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, int.Parse(_config.GetSection("EmailHostPort").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailHostUserName").Value, _config.GetSection("EmailHostPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}
