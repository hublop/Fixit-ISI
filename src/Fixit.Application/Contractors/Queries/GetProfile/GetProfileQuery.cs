using Fixit.Shared.CQRS;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class GetProfileQuery : IQuery<ContractorProfile>
    {
        public int ContractorId { get; set; }
    }
}