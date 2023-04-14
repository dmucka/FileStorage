namespace FileStorageBL.DTOs
{
    public class VersionedFileCreateDto : BaseDto
    {
        public string Name { get; set; }
        public int? NewestVersionId { get; set; }
        public int FolderId { get; set; }
    }
}