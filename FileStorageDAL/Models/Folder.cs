using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class Folder : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public DateTime? Deleted { get; set; }

        // parent folder
        public int? FolderId { get; set; }

        // reflexivna zavislost
        public ICollection<Folder> Folders { get; set; }

        public ICollection<VersionedFile> VersionedFiles { get; set; }

        [Required]
        public User Owner { get; set; }

    }
}