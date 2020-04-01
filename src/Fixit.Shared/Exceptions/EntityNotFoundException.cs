namespace Fixit.Shared.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName, int id)
            : base("Object not found.", $"{entityName} with id: {id} has not been found.")
        {
        }
    }
}