using Fixit.Domain.Common;

namespace Fixit.Application.Categories.Exceptions
{
    public class CategoryDoesNotExistException : DomainException
    {
        public CategoryDoesNotExistException(int categoryId) : base("Category does not exist.",
            $"Category with id {categoryId} does not exist.")
        {
        }
    }
}