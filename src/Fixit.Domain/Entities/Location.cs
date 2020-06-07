using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class Location : Entity
    {
        public string PlaceId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public ICollection<Contractor> Contractors { get; set; }
        public ICollection<Order> Orders { get; set; }  
    }
}