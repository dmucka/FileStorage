using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class VersionedFileUser : BaseModel
    {
        [Required]
        public int VersionedFileId { get; set; }

        [Required]
        public int UserId { get; set; }

        public VersionedFile VersionedFile { get; set; }

        public User User { get; set; }
    }
}
