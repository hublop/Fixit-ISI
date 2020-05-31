using System;
using System.Collections.Generic;
using System.Text;

namespace Fixit.Domain.Entities
{
  public class DistributedOrderContractor: Entity
  {
    public int ContractorId { get; set; }
    public int OrderId { get; set; }

    public Contractor Contractor { get; set; }
    public Order Order { get; set; }
  }
}
