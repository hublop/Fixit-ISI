using System.Threading.Tasks;

namespace Fixit.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string content);
        Task SendEmailAsync(string from, string to, string subject, string content);
    }
}