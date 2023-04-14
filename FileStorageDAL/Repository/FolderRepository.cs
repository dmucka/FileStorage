using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class FolderRepository : Repository<Folder>
    {
        public FolderRepository(FileStorageContext context) : base(context)
        {
        }

        public override async Task<Folder> Get(int id)
        {
            return await _dbSet
                .Include(folder => folder.Folders)
                .Include(folder => folder.VersionedFiles).ThenInclude(vf => vf.NewestVersion)
                .Include(folder => folder.Owner)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<List<Folder>> GetAll()
        {
            return await _dbSet
                .Include(folder => folder.Folders)
                .Include(folder => folder.VersionedFiles).ThenInclude(vf => vf.NewestVersion)
                .Include(folder => folder.Owner)
                .ToListAsync();
        }

        public async Task<List<Folder>> GetAllByUserId(int userId)
        {
            return await _dbSet
                .Include(folder => folder.Folders)
                .Include(folder => folder.VersionedFiles).ThenInclude(vf => vf.NewestVersion)
                .Include(folder => folder.Owner)
                .Where(folder => folder.Owner.Id == userId)
                .ToListAsync();
        }

        public async Task<List<Folder>> GetAllRootByUserId(int userId)
        {
            return await _dbSet
                .Include(folder => folder.Folders)
                .Include(folder => folder.VersionedFiles).ThenInclude(vf => vf.NewestVersion)
                .Include(folder => folder.Owner)
                .Where(folder => folder.Owner.Id == userId && folder.FolderId == null)
                .ToListAsync();
        }
    }
}
