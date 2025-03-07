namespace Shared.Exceptions
{
    public class BadRequestServerException : Exception
    {

        public BadRequestServerException(string message) : base(message)
        {
             
        }

        public BadRequestServerException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
