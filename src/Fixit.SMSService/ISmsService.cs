using System.Threading.Tasks;

namespace Fixit.SmsService
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string content);
    }
}