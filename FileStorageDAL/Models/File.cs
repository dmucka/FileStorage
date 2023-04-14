using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    /// <summary>
    /// Skutocna reprezentacia suboru na disku
    /// </summary>
    public class File : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Link { get; set; }

        [Required]
        public int Size { get; set; }

        public int FileVersionId { get; set; }

        public FileVersion FileVersion { get; set; }
    }
}