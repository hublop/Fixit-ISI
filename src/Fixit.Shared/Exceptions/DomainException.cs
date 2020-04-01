using System;

namespace Fixit.Shared.Exceptions
{
    public class DomainException : Exception
    {
        public string Details { get; set; }

        public DomainException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}