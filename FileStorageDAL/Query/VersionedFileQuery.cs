using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public class VersionedFileQuery : Query<VersionedFile>
    {
        public VersionedFileQuery(FileStorageContext context) : base(context)
        {
            _query = _query
                .Include(log => log.FileVersions);
        }
    }
}
