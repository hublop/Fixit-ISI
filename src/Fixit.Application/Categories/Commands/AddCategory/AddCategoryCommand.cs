using System.Collections.Generic;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommand : ICommand<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SubcategoryInAddCategoryCommand> Subcategories { get; set; }
    }

    public class SubcategoryInAddCategoryCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}