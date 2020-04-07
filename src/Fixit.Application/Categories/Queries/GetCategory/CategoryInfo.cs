using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Categories.Queries.GetCategory
{
    public class CategoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SubcategoryInfo> SubCategories { get; set; }
    }

    public class GetCategoryQueryMapping : Profile
    {
        public GetCategoryQueryMapping()
        {
            CreateMap<Category, CategoryInfo>()
                .ForMember(dest => dest.SubCategories, opts =>
                    opts.MapFrom(src => src.SubCategories.OrderBy(x => x.Name)));

            CreateMap<Subcategory, SubcategoryInfo>();
        }
    }
}