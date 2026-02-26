namespace Tutorium.Shared.Utils.Redis.Abstractions
{
    public interface IWithGuidToken
    {
        Guid Token { get; }
    }
}
