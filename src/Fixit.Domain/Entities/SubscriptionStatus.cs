using System;
using System.Collections.Generic;
using System.Text;

namespace Fixit.Domain.Entities
{
  public class SubscriptionStatus: Entity
  {
    public string Status { get; set; }

    public ICollection<Contractor> Contractors { get; set; }
  }
}
