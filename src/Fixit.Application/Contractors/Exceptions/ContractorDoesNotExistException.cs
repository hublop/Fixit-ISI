using Fixit.Domain.Common;

namespace Fixit.Application.Contractors.Exceptions
{
    public class ContractorDoesNotExistException : DomainException
    {
        public ContractorDoesNotExistException(int contractorId)
            : base("Contractor has not been found.",
                $"Contractor with id: {contractorId} does not exist.")
        {
        }
    }
}