using Fixit.Domain.Common;

namespace Fixit.Application.Common.Exceptions
{
    public class UserUnauthorizedException : DomainException
    {
        public UserUnauthorizedException(int userId)
            : base("No permissions.", $"User with id {userId} does not have permissions to perform this action.")
        {

        }
    }
}