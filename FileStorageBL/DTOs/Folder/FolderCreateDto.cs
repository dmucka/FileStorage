using FileStorageDAL.Models;

namespace FileStorageBL.DTOs
{
    public class FolderCreateDto : BaseDto
    {
        public string Name { get; set; }
        public User Owner { get; set; }
        public int? FolderId { get; set; }
    }
}
