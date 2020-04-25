using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Image : Entity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public ICollection<OrderImage> OrderImages { get; set; }
        public ICollection<User> UserImages { get; set; }
    }
}