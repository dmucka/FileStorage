using FileStorageDAL.Models;
using System.Collections.Generic;

namespace FileStorageBL.DTOs
{
    public class FolderShowDto : BaseDto
    {
        public string Name { get; set; }
        //public DateTime? Deleted { get; set; }
        public User Owner { get; set; }
        // TODO?
        public ICollection<Folder> Folders { get; set; }
        public ICollection<VersionedFile> VersionedFiles { get; set; }

        public FolderShowDto()
        {
            Folders = new List<Folder>();
            VersionedFiles = new List<VersionedFile>();
            //Owner = new User();
        }
    }
}
