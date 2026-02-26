namespace Tutorium.Shared.Utils.Redis.Abstractions
{
    public interface IRuntimeRepository<T> where T : class, IWithGuidToken
    {
        Task Add(T model);
        Task Update(T model);
        Task<T?> GetByTokenAsync(Guid token);
    }
}
