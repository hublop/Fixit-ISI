using System.Collections.Generic;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Queries.GetOffersForCustomer
{
    public class GetOffersForCustomerQuery : IQuery<List<OrderOfferForCustomer>>
    {
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
    }
}