using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Categories.Queries.GetCategories
{
    public class CategoryInfoForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SubcategoryInfoForList> SubCategories { get; set; }
    }

    public class GetAllCategoriesWithSubcategoriesQueryMapping : Profile
    {
        public GetAllCategoriesWithSubcategoriesQueryMapping()
        {
            CreateMap<Subcategory, SubcategoryInfoForList>();

            CreateMap<Category, CategoryInfoForList>()
                .ForMember(dest => dest.SubCategories, opts =>
                    opts.MapFrom(src => src.SubCategories.OrderBy(x => x.Name)));


        }
    }
}