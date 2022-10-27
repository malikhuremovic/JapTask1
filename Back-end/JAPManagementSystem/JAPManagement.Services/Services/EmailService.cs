using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.SelectionModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace JAPManagement.Services.Services
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
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfirmation:EmailHostUserName").Value));
                email.To.Add(MailboxAddress.Parse(student.Email));
                email.Subject = "JAP Program";
                string EmailText = "<div style=\"text-align: center;\"><img style=\"width: 70%; height: auto;\" src=\"" + _config.GetSection("EmailConfirmation:EmailImageURL").Value
                    +
                    "\" /></div><div style=\"width: 100%\"><h4>Hello " + student.FirstName + ", congratulations on becoming a Mistralovac! :)</h4><p style=\"color:#666666\">We are pleased to announce that you are now officialy our JAP fellow. We can not wait to have you onboard! <br/><br/>All of our JAP students can access their account on the JAP Management Platform. <br/>Please, find your credentials below &darr;<br/><br/>Username: <strong>" + student.UserName + "</strong><br/>Password: <strong>" + student.Password + "</strong><br/><br/>You can access the platform by clicking <a href=\"" + _config.GetSection("EmailConfirmation:EmailJAPPlatformURL").Value +
                    "\" style=\"text-decoration:underline; color: #689ed1; font-weight:bold;\">here</a>.<br/><br/>Thank you for choosing Mistral!<br/><br/>All the best,<br/>Malik Huremović</p></div><div style=\"width: 70%; height: 4vh; text-align:center; border-radius: 5px; font-family:sans-serif; color:#fff; font-weight: 700; padding: 8px; margin: 0px auto; margin-top: 20px; display: flex; align-items: center; justify-content: center; align-content: center; background-color:#689ed1;\"><a style=\"color: #fff; text-decoration:none; font-weight: bold;\" href=\"" + _config.GetSection("EmailConfirmation:EmailCompanyURL").Value +
                    "\">Learn more about company culture</a></div>";
                email.Body = new TextPart(TextFormat.Html) { Text = EmailText };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    _config.GetSection("EmailConfirmation:EmailHost").Value,
                    int.Parse(_config.GetSection("EmailConfirmation:EmailHostPort").Value),
                    SecureSocketOptions.StartTls);
                smtp.Authenticate(
                    _config.GetSection("EmailConfirmation:EmailHostUserName").Value,
                    _config.GetSection("EmailConfirmation:EmailHostPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public void SendConfirmationEmail(AdminUserCreatedDto admin)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfirmation:EmailHostUserName").Value));
                email.To.Add(MailboxAddress.Parse(admin.Email));
                email.Subject = "JAP Program";
                string EmailText = "<div style=\"text-align: center;\"><img style=\"width: 40%; height: auto;\" src=\"" + _config.GetSection("EmailConfirmation:EmailJAPImageURL").Value
                    +
                    "\" /></div><div style=\"width: 100%\"><h4>Hello " + admin.FirstName + ", you have been added as an Admin on JAP Management Platform :)</h4>Please, find your credentials below &darr;<br/><br/>Username: <strong>" + admin.UserName + "</strong><br/>Password: <strong>" + admin.Password + "</strong><br/><br/>You can access the platform by clicking <a href=\"" + _config.GetSection("EmailConfirmation:EmailJAPPlatformURL").Value +
                    "\" style=\"text-decoration:underline; color: #689ed1; font-weight:bold;\">here</a>.<br/><br/>Have a nice work hours!!<br/><br/>All the best,<br/>Malik Huremović</p></div>";
                email.Body = new TextPart(TextFormat.Html) { Text = EmailText };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    _config.GetSection("EmailConfirmation:EmailHost").Value,
                    int.Parse(_config.GetSection("EmailConfirmation:EmailHostPort").Value),
                    SecureSocketOptions.StartTls);
                smtp.Authenticate(
                    _config.GetSection("EmailConfirmation:EmailHostUserName").Value,
                    _config.GetSection("EmailConfirmation:EmailHostPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public void SendConfirmationEmail(AdminReport report)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfirmation:EmailHostUserName").Value));
                email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailConfirmation:EmailReportReciever").Value));
                email.Subject = "JAP Program | Selection " + report.SelectionName + " has come to an end!";
                string EmailText = "<div style=\"text-align: center;\"><img style=\"width: 40%; height: auto;\" src=\"" + _config.GetSection("EmailConfirmation:EmailJAPImageURL").Value
                    +
                    "\" /></div><br/><br/><p style=\font-size: 20px;\"><span style=\"font-size:25px;\">Hello Dear Program Coordinator!</span><br/><br/>Good news knocking on the door yet again!<br/>Today, on " + dateTime.Day.ToString() + "th of " + dateTime.ToString("MMMM") + ", the selection <strong>" + report.SelectionName + "</strong> has come to an end.</p> <h4>You can check the statistics below &darr;</h4><br /> <div style=\"display:inline-block; min-width: 90%; padding: 25px; background-color: #5c9af8; color: #fff; font-size: 20px\">The selection success rate is: " + report.SelectionSuccessRate
                    + "%</div> <div style=\"display:inline-block; min-width: 90%; padding: 25px; background-color: #64ac8b; color: #fff; font-size: 20px\">The overall success rate is: " + report.OverallSuccessRate + "%</div><div><br/>All the best, <br /><br />Malik Huremović<br/><em>JAP Platform Admin</em></div>";
                email.Body = new TextPart(TextFormat.Html) { Text = EmailText };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    _config.GetSection("EmailConfirmation:EmailHost").Value,
                    int.Parse(_config.GetSection("EmailConfirmation:EmailHostPort").Value),
                    SecureSocketOptions.StartTls);
                smtp.Authenticate(
                    _config.GetSection("EmailConfirmation:EmailHostUserName").Value,
                    _config.GetSection("EmailConfirmation:EmailHostPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}
