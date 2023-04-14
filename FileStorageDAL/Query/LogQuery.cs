using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public class LogQuery : Query<Log>
    {
        public LogQuery(FileStorageContext context) : base(context)
        {
            _query = _query
                .Include(log => log.File)
                .Include(log => log.User);
        }
    }
}
