using Fixit.Domain.Common;

namespace Fixit.Application.Contractors.Exceptions
{
  public class UserAlreadyExistsException : DomainException
  {
    public UserAlreadyExistsException(string email) : base("User already exist.",
      $"User with email: {email} already exist.")
    {
    }
  }
}
