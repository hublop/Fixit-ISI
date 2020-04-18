using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fixit.WebApi.Orders.DTOs
{
    public class CancelOrderCommandDto
    {
        public int CustomerId { get; set; }
    }
}
