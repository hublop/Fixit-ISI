using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Commands.AddRepairService
{
    public class AddRepairServiceCommand : ICommand
    {
        public int ContractorId { get; set; }
        public int SubcategoryId { get; set; }
    }
}