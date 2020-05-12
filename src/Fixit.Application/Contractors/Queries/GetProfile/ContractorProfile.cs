using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class ContractorProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string SelfDescription { get; set; }
        public DateTime ContractorFrom { get; set; }
        public string PlaceId { get; set; }
        public double AvgQuality { get; set; }
        public double AvgPunctuality { get; set; }
        public double AvgInvolvement { get; set; }
        public double AvgRating { get; set; }
        public string ImageUrl { get; set; }
        public IList<CategoryInProfile> Categories { get; set; }
        public IList<OpinionsInProfile> Opinions { get; set; }
        public IList<RepairService> RepairServices { get; set; }
        public string ContractorUUID { get; set; }
  }

    public class GetProfileQueryMapping : Profile
    {
        public GetProfileQueryMapping()
        {
            CreateMap<Contractor, ContractorProfile>()
                .ForMember(dest => dest.ImageUrl, opts =>
                    opts.MapFrom(src => src.Image == null ? null : src.Image.Url))
                .ForMember(dest => dest.PlaceId, opts =>
                    opts.MapFrom(src => src.Location.PlaceId))
                .ForMember(dest => dest.AvgQuality, opts =>
                    opts.MapFrom(x => x.GetQualityAverage()))
                .ForMember(dest => dest.AvgInvolvement, opts =>
                    opts.MapFrom(x => x.GetInvolvementAverage()))
                .ForMember(dest => dest.AvgPunctuality, opts =>
                    opts.MapFrom(x => x.GetPunctualityAverage()))
                .ForMember(dest => dest.AvgRating, opts =>
                    opts.MapFrom(x => x.GetAvgRating()))
                .ForMember(dest => dest.Opinions, opts =>
                    opts.MapFrom(x => x.RepairServices.Where(y => y.Opinions.Any()).SelectMany(y => y.Opinions)
                        .OrderByDescending(y => y.CreatedOn)))
                .ForMember(dest => dest.AvgRating, opts =>
                    opts.MapFrom(x => x.GetAvgRating()))
                .ForMember(dest => dest.Categories, opts =>
                    opts.MapFrom(x => x.RepairServices
                        .Select(s => s.SubCategory)
                            .Select(y => y.Category)
                            .Distinct()
                            .OrderBy(n => n.Name)));

            CreateMap<Opinion, OpinionsInProfile>()
                .ForMember(dest => dest.Subcategory, opts =>
                    opts.MapFrom(x => x.RepairService
                        .SubCategory));

            CreateMap<Subcategory, SubcategoryInProfile>();

            CreateMap<Category, CategoryInProfile>()
                .ForMember(dest => dest.Subcategories, opts =>
                    opts.MapFrom(x => x.SubCategories.OrderBy(y => y.Name)));

            CreateMap<RepairService, RepairServiceInProfile>();

            CreateMap<Customer, CustomerInProfile>();

            CreateMap<Rating, RatingInProfile>();

        }
    }
}