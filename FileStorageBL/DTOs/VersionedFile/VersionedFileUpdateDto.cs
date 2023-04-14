namespace FileStorageBL.DTOs
{
    public class VersionedFileUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public int? NewestVersionId { get; set; }
    }
}
