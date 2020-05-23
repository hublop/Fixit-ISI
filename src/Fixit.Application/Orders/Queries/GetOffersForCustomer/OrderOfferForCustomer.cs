using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Orders.Queries.GetOffersForCustomer
{
    public class OrderOfferForCustomer
    {
        public OrderInOffer Order { get; set; }
        public ContractorInOffer Contractor { get; set; }
        public string Comment { get; set; }
        public double PredictedPrice { get; set; }
    }

    public class OrderInOffer
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

    public class ContractorInOffer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class GetOffersForCustomerQueryMapping : Profile
    {
        public GetOffersForCustomerQueryMapping()
        {
            CreateMap<Contractor, ContractorInOffer>()
                .ForMember(dest => dest.PhotoUrl, opts => 
                    opts.MapFrom(src => src.ImageId.HasValue ? src.Image.Url : null));

            CreateMap<Order, OrderInOffer>()
                .ForMember(dest => dest.PlaceId, opts =>
                    opts.MapFrom(src => src.Location.PlaceId))
                .ForMember(dest => dest.SubcategoryName, opts =>
                    opts.MapFrom(src => src.Subcategory.Name))
                .ForMember(dest => dest.CategoryName, opts =>
                    opts.MapFrom(src => src.Subcategory.Category.Name))
                .ForMember(dest => dest.CategoryId, opts =>
                    opts.MapFrom(src => src.Subcategory.CategoryId))
                .ForMember(dest => dest.PhotoUrls, opts =>
                    opts.MapFrom(src => src.OrderImages
                        .Select(x => x.Image)
                        .Select(x => x.Url)
                        .Distinct()));

            CreateMap<OrderOffer, OrderOfferForCustomer>();
        }
    }
}