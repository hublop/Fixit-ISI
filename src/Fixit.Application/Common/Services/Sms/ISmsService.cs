using System.Threading.Tasks;

namespace Fixit.Application.Common.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string content);
    }
}