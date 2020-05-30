using Fixit.Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fixit.Application.Orders.Queries.GetOrdersForContractor
{
  public class GetOrdersForContractorQuery: IQuery<List<OrderOfferForContractor>>
  {
    public int ContractorId { get; set; }
  }
}
