using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Customer : User
    {
        public ICollection<Order> Orders { get; set; }
    }
}