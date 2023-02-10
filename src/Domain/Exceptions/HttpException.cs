namespace NotificationProcessor.Domain.Exceptions
{
    public class HttpException : BaseException
    {
        public HttpException(string message) : base(message)
        {
        }
    }
}
