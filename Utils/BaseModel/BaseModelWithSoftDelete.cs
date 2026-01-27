namespace Tutorium.Shared.Utils.BaseModel
{
    public abstract class BaseModelWithSoftDelete : BaseModel
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
