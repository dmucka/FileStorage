using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class FileRepository : Repository<File>
    {
        public FileRepository(FileStorageContext context) : base(context)
        {
        }

        public override async Task<File> Get(int id)
        {
            return await _dbSet
                .Include(file => file.FileVersion)
                .ThenInclude(fv => fv.VersionedFile)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<List<File>> GetAll()
        {
            return await _dbSet
                .Include(file => file.FileVersion)
                .ToListAsync();
        }
    }
}
