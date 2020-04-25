using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Customer
    {
        public ICollection<Order> Orders { get; set; }
    }
}