using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Commands.UpdatePersonalData
{
    public class UpdateContractorPersonalDataCommand : ICommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string SelfDescription { get; set; }
        public string PlaceId { get; set; }
    }
}