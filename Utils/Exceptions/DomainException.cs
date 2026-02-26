namespace Tutorium.Shared.Utils.Exceptions
{
    public abstract class DomainException : AppException
    {
        protected DomainException(string message, string errorCode) : base(message, errorCode) { }
    }
}
