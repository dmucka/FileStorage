using FileStorageDAL.Models;

namespace FileStorageBL.DTOs
{
    public class FolderUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public User Owner { get; set; }
        public int? FolderId { get; set; }
    }
}
