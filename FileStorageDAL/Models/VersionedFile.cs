using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    /// <summary>
    /// Abstrakcia suboru pre cloudove ulozisko, obsahuje odkaz na mnozinu verzii
    /// </summary>
    public class VersionedFile : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public DateTime? Deleted { get; set; }

        public ICollection<FileVersion> FileVersions { get; set; }

        public int? NewestVersionId { get; set; }

        public FileVersion NewestVersion { get; set; }

        public ICollection<User> SharedUsers { get; set; }

        // parent folder
        [Required]
        public int FolderId { get; set; }
    }
}
