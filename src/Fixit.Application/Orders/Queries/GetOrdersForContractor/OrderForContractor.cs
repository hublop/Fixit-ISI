using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Fixit.Application.Orders.Queries.GetOffersForCustomer;
using Fixit.Domain.Entities;

namespace Fixit.Application.Orders.Queries.GetOrdersForContractor
{
  public class OrderForContractor
  {
      public int Id { get; set; }
      public string Description { get; set; }
      public string PlaceId { get; set; }
      public string SubcategoryName { get; set; }
      public int SubcategoryId { get; set; }
      public string CategoryName { get; set; }
      public int CategoryId { get; set; }
      public DateTime CreationDate { get; set; }
      public List<string> PhotoUrls { get; set; }

  }
  public class GetOrdersForContractorQueryMapping : Profile
  {
    public GetOrdersForContractorQueryMapping()
    {
        CreateMap<Order, OrderForContractor>();
    }
  }
}
