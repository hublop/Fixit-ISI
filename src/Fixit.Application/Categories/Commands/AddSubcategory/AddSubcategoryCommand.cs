using Fixit.Shared.CQRS;

namespace Fixit.Application.Categories.Commands.AddSubcategory
{
    public class AddSubcategoryCommand : ICommand<int>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}