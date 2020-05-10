using Fixit.Domain.Common;

namespace Fixit.Application.Contractors.Exceptions
{
    public class SubcategoryDoesNotExistException : DomainException
    {
        public SubcategoryDoesNotExistException(int subcategoryId)
            : base("Subcategory has not been found.",
                $"Subcategory with id: {subcategoryId} does not exist.")
        {
        }
    }
}