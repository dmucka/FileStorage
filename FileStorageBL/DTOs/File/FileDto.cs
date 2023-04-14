namespace FileStorageBL.DTOs
{
    public class FileDto : BaseDto
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int Size { get; set; }
        public int FileVersionId { get; set; }
    }
}
