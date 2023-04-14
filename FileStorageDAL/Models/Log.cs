using FileStorageDAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FileStorageDAL.Models
{
    public class Log : BaseModel
    {
        [Required]
        public LogOperation Operation { get; set; }

        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FileId { get; set; }

        public User User { get; set; }

        public File File { get; set; }
    }
}
