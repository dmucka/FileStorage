using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
