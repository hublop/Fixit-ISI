using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Contractors.Queries.GetList
{
    public class ContractorForList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PlaceId { get; set; }
        public double AvgRating { get; set; }
        public int OpinionsCount { get; set; }
        public string NewestOpinion { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Specializations { get; set; }
        public string ContractorUUID { get; set; }
  }

    public class GetContractorsListQueryMapping : Profile
    {
        public GetContractorsListQueryMapping()
        {
            CreateMap<Contractor, ContractorForList>()
                .ForMember(dest => dest.PlaceId, opts =>
                    opts.MapFrom(src => src.Location.PlaceId))
                //.ForMember(dest => dest.City, opts =>
                //    opts.MapFrom(src => src.Location.City))
                .ForMember(dest => dest.AvgRating, opts =>
                    opts.MapFrom(src => src.GetAvgRating()))
                .ForMember(dest => dest.ImageUrl, opts =>
                    opts.MapFrom(src =>
                        src.Image == null
                            ? null
                            : src.Image.Url))
                .ForMember(dest => dest.OpinionsCount, opts =>
                    opts.MapFrom(src => src.RepairServices.SelectMany(x => x.Opinions).Count()))
                .ForMember(dest => dest.NewestOpinion, opts =>
                    opts.MapFrom(src =>
                        !src.RepairServices.SelectMany(x => x.Opinions).Any()
                            ? null
                            : src.RepairServices.SelectMany(x => x.Opinions).OrderByDescending(y => y.CreatedOn).FirstOrDefault().Comment))
                .ForMember(dest => dest.Specializations, opts =>
                    opts.MapFrom(src => src.RepairServices
                        .Select(x => x.SubCategory.Category.Name)
                        .Distinct()
                        .OrderBy(x => x)
                    ));
        }
    }
}