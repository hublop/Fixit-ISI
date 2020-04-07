using Fixit.Shared.CQRS;

namespace Fixit.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IQuery<CategoryInfo>
    {
        public int Id { get; set; }
    }
}