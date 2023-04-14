using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FileStoragePL.Pages.Models
{
    public class UploadedFile
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}
