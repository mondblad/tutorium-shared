namespace Tutorium.Shared.Utils.Exceptions
{
    public abstract class ApplicationException : AppException
    {
        protected ApplicationException(string message, string errorCode) : base(message, errorCode) { }
    }
}
