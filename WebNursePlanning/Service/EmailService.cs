using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

namespace WebNursePlanning.Service
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();

            /*
             #Mail

            mail.host=smtp.gmail.com
            mail.port=587
            mail.username=gdm.traitement.nuit@gmail.com
            mail.password=gestionMission

            # Other properties
            mail.properties.mail.smtp.auth=true
            mail.properties.mail.smtp.connectiontimeout=5000
            mail.properties.mail.smtp.timeout=5000
            mail.properties.mail.smtp.writetimeout=5000

            # TLS , port 587
            mail.properties.mail.smtp.starttls.enable=true
             */
        }
    }
}