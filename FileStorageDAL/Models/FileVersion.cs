using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    /// <summary>
    /// Kazdy VersionedFile obsahuje mnozinu FileVersions, ktora definuje historiu toho suboru
    /// </summary>
    public class FileVersion : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Number { get; set; }

        [MaxLength(1024)]
        public string Changelog { get; set; }

        [Required]
        public VersionedFile VersionedFile { get; set; }

        public File File { get; set; }

        [Required]
        public int VersionedFileId { get; set; }
    }
}