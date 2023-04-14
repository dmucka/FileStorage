using FileStorageDAL.Models;
using System.Collections.Generic;

namespace FileStorageBL.DTOs
{
    public class UserCreateDto : BaseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }

        public UserCreateDto()
        {
            Roles = new List<Role>();
        }
    }
}
