using Fixit.Shared.CQRS;

namespace Fixit.Application.Customers.Commands.UpdatePersonalData
{
    public class UpdateCustomerPersonalDataCommand : ICommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}