using Fixit.Domain.Common;

namespace Fixit.Application.Contractors.Exceptions
{
  public class RegisteringUserException : DomainException
  {
    public RegisteringUserException(string email) : base("Failed to register user.",
      $"Failed to regiser user with email: {email}")
    {
    }
  }
}
