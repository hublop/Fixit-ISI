using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Customers.Commands.RegisterCustomer
{
  public class RegisterCustomerCommand: ICommand
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
  }
}
