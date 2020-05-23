using System.Collections.Generic;

namespace Fixit.WebApi.Orders.DTOs
{
    public class CreateDirectOrderCommandDto
    {
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public int CustomerId { get; set; }
        public string PlaceId { get; set; }
        public List<string> Base64Photos { get; set; }
    }
}
