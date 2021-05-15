using System;

namespace DavidMorales.Domain.Exceptions
{
    public class AppAuthException : Exception
    {
        public AppAuthException()
        {
        }

        public AppAuthException(string message)
            : base(message)
        {
        }

        public AppAuthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
