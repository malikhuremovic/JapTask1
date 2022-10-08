using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
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

        public void SendConfirmationEmail(StudentUserCreatedDto student)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailHostUserName").Value));
                email.To.Add(MailboxAddress.Parse(student.Email));
                email.Subject = "JAP Program";
                email.Body = new TextPart(TextFormat.Html) { Text = "<div style=\"text-align: center;\"><img style=\"width: 90%; height: auto;\" src=\"https://media-exp1.licdn.com/dms/image/C4D22AQE2RZ-33wBT9A/feedshare-shrink_800/0/1659966643375?e=2147483647&v=beta&t=KA0lze3P08o3jynQkBazk4r5uyCBld3s5uRKHhkjb70\" /></div><div style=\"width: 100%\"><h4>Hello " + student.FirstName + ", congratulations on becoming a Mistralovac! :)</h4><p style=\"color:#666666\">We are pleased to announce that you are now officialy our JAP fellow. We can not wait to have you onboard! <br/><br/>All of our JAP students can access their account on the JAP Management Platform. <br/>Please, find your credentials below &darr;<br/><br/>Username: <strong>" + student.UserName +"</strong><br/>Password: <strong>" + student.Password + "</strong><br/><br/>You can access the platform by clicking <a href=\"http://localhost:3000\" style=\"text-decoration:underline; color: #689ed1; font-weight:bold;\">here</a>.<br/><br/>Thank you for choosing Mistral!<br/><br/>All the best,<br/>Malik Huremović</p></div><div style=\"width: 70%; height: 4vh; text-align:center; border-radius: 5px; font-family:sans-serif; color:#fff; font-weight: 700; padding: 8px; margin: 0px auto; margin-top: 20px; display: flex; align-items: center; justify-content: center; align-content: center; background-color:#689ed1;\"><a style=\"color: #fff; text-decoration:none; font-weight: bold;\" href=\"https://www.mistral.ba\">Learn more about company culture</a></div>"
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
