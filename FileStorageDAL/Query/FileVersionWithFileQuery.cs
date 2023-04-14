using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public class FileVersionWithFileQuery : Query<FileVersion>
    {
        public FileVersionWithFileQuery(FileStorageContext context) : base(context)
        {
            _query = _query
                .Include(log => log.File);
        }
    }
}
