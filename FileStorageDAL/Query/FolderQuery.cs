using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FileStorageDAL.Query
{
    public class FolderQuery : Query<Folder>
    {
        public FolderQuery(FileStorageContext context) : base(context)
        {
            _query = _query
                .Include(folder => folder.Folders)
                .Include(folder => folder.VersionedFiles);
        }

        public void Where(int id)
        {
            _query = _query.Where(x => x.Owner.Id == id);
        }
    }
}
