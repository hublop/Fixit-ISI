using System;

namespace Fixit.Application.Common.Exceptions
{
    public class AddFailureException : Exception
    {
        public AddFailureException(string name, string message = "")
            : base($"Adding of entity \"{name}\" failed. {message}")
        {
        }
    }
}