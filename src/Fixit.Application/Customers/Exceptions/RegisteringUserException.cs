using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Domain.Common;

namespace Fixit.Application.Customers.Exceptions
{
  public class RegisteringUserException : DomainException
  {
    public RegisteringUserException(string email) : base("Failed to register user.",
      $"Failed to regiser user with email: {email}")
    {
    }
  }
}
