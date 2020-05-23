using System;
using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Order : Entity
    {
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public int SubcategoryId { get; set; }
        public bool IsDistributed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastNotificationDate { get; set; }

        public Customer Customer { get; set; }
        public Subcategory Subcategory { get; set; }
        public Location Location { get; set; }
        public ICollection<OrderOffer> OrderOffers { get; set; }
        public ICollection<OrderImage> OrderImages { get; set; }
    }
}