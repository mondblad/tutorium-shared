namespace Tutorium.Shared.Utils.BaseModel
{
    public interface IBaseModel
    {
        public int Id { get; set; }
    }

    public abstract class BaseModel : IBaseModel
    {
        public int Id { get; set; }
    }
}
