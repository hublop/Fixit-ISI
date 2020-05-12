using Fixit.Domain.Common;

namespace Fixit.Application.Customers.Exceptions
{
    public class CustomerDoesNotExistException : DomainException
    {
        public CustomerDoesNotExistException(int id) : base("Customer does not exist.",
            $"Customer iwth id: {id} does not exist.")
        {
        }
    }
}