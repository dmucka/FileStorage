namespace FileStorageBL.DTOs
{
    public class UserShowDto : BaseDto
    {
        public string Username { get; set; }
        public string[] Roles { get; set; }
    }
}
