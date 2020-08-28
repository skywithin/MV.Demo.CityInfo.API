using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private string _mailTo;
        private string _mailFrom;

        public CloudMailService(IConfiguration configuration)
        {
            _configuration = configuration;

            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            //send email - output to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
