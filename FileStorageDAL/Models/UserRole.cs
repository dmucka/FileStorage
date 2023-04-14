using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class UserRole : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
