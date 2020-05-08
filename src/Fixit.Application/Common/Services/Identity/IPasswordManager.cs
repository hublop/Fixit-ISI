using System.Threading.Tasks;

namespace Fixit.Application.Common.Services.Identity
{
    public interface IPasswordManager
    {
        Task<bool> IsPasswordValid(int userId, string password);
        Task<bool> UpdatePassword(int userId, string oldPassword, string newPassword);
    }
}