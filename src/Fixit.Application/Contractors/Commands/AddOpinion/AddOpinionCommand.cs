using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Commands.AddOpinion
{
    public class AddOpinionCommand : ICommand
    {
        public double Punctuality { get; set; }
        public double Quality { get; set; }
        public double Involvement { get; set; }
        public string Comment { get; set; }
        public int ContractorId { get; set; }
        public int SubcategoryId { get; set; }
    }
}