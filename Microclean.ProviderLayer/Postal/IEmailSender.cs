using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.ProviderLayer.Postal
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, string cc = null);
    }
}
