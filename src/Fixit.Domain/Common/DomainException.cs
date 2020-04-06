using System;

namespace Fixit.Domain.Common
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