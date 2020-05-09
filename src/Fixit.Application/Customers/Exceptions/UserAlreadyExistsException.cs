using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Domain.Common;

namespace Fixit.Application.Customers.Exceptions
{
  public class UserAlreadyExistsException : DomainException
  {
    public UserAlreadyExistsException(string email) : base("User already exist.",
      $"User with email: {email} already exist.")
    {
    }
  }
}
