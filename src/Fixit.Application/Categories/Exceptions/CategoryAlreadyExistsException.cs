using Fixit.Domain.Common;

namespace Fixit.Application.Categories.Exceptions
{
    public class CategoryAlreadyExistsException : DomainException
    {
        public CategoryAlreadyExistsException(string name) : base("Duplicate category name.",
            $"Category with name {name} already exists.")
        {
        }
    }
}