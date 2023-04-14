using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class VersionedFileRepository : Repository<VersionedFile>
    {
        public VersionedFileRepository(FileStorageContext context) : base(context)
        {
        }

        public override async Task<VersionedFile> Get(int id)
        {
            return await _dbSet
                .Include(versionedFile => versionedFile.FileVersions).ThenInclude(fileVersion => fileVersion.File)
                .Include(versionedFile => versionedFile.SharedUsers)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<List<VersionedFile>> GetAll()
        {
            return await _dbSet
                .Include(versionedFile => versionedFile.FileVersions).ThenInclude(fileVersion => fileVersion.File)
                .Include(versionedFile => versionedFile.SharedUsers)
                .ToListAsync();
        }
    }
}
