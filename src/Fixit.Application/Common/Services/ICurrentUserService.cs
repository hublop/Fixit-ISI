namespace Fixit.Application.Common.Services
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }

        bool IsUser(int userid);
    }
}