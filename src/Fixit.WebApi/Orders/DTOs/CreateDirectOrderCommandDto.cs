using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fixit.WebApi.Orders.DTOs
{
    public class CreateDirectOrderCommandDto
    {
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public int CustomerId { get; set; }
        public int PlaceId { get; set; }
    }
}
