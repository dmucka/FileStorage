using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public class UserWithFoldersQuery : Query<User>
    {
        public UserWithFoldersQuery(FileStorageContext context) : base(context)
        {
            _query = _query.Include(user => user.Folders);
        }
    }
}
