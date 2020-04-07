using Fixit.Shared.CQRS;

namespace Fixit.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommand : ICommand
    {
        public int Id { get; set; }
    }
}