using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(FileStorageContext context) : base(context)
        {
        }

        public override async Task<Role> Get(int id)
        {
            return await _dbSet
                .Include(role => role.Users)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Role> GetByName(string name)
        {
            return await _dbSet
                .Include(role => role.Users)
                .FirstOrDefaultAsync(item => item.Name == name);
        }
    }
}
