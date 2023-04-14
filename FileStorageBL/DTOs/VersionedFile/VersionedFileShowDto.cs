using FileStorageDAL.Models;
using System.Collections.Generic;

namespace FileStorageBL.DTOs
{
    public class VersionedFileShowDto : BaseDto
    {
        public string Name { get; set; }
        public int NewestVersionId { get; set; }
        public ICollection<FileVersion> FileVersions { get; set; }

        public VersionedFileShowDto()
        {
            FileVersions = new List<FileVersion>();
        }
    }
}
