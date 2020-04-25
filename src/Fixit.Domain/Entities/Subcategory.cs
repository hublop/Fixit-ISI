using System;
using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Subcategory : Entity
    {
        public Subcategory(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}