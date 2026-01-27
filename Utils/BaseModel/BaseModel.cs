using System.ComponentModel.DataAnnotations;

namespace Tutorium.Shared.Utils.BaseModel
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
