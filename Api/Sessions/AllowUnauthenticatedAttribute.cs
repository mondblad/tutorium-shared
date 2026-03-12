namespace Tutorium.Shared.Api.Sessions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AllowUnauthenticatedAttribute : Attribute
    {
    }
}
