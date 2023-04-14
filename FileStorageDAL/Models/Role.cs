using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class Role : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
