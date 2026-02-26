namespace Tutorium.Shared.Utils.Exceptions
{
    public abstract class InfrastructureException : AppException
    {
        protected InfrastructureException(string message, string errorCode) : base(message, errorCode) { }
    }
}
