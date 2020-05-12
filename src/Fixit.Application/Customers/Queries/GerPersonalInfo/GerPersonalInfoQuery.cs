using Fixit.Shared.CQRS;

namespace Fixit.Application.Customers.Queries.GerPersonalInfo
{
    public class GetPersonalInfoQuery : IQuery<CustomerPersonalInfo>
    {
        public int CustomerId { get; set; }
    }
}