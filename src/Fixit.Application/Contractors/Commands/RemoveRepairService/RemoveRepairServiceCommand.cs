using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Commands.RemoveRepairService
{
    public class RemoveRepairServiceCommand : ICommand
    {
        public int ContractorId { get; set; }
        public int SubcategoryId { get; set; }
    }
}