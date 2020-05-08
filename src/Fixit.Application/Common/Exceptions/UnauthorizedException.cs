using System;

namespace Fixit.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string Details { get; set; }

        public UnauthorizedException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}