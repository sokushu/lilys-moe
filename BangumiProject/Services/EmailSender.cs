using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return new Task(() => 
            {
                File.WriteAllLines("C:\\Users\\mi\\Desktop\\TestEmail.txt", new string[] { email, subject, htmlMessage });
            });
        }
    }
}
