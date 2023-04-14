namespace FileStorageBL.DTOs
{
    public class FileVersionDto : BaseDto
    {
        public string Number { get; set; }
        public string Changelog { get; set; }
        public int VersionedFileId { get; set; }
    }
}
