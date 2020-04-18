using System.Threading.Tasks;

namespace Fixit.Application.Common.Services.Sms
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string content);
    }
}