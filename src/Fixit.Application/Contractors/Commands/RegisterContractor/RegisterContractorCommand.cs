using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Commands.RegisterContractor
{
  public class RegisterContractorCommand : ICommand
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string PlaceId { get; set; }
  }
}
