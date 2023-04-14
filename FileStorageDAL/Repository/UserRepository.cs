using FileStorageDAL.Enums;
using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(FileStorageContext context) : base(context)
        {
        }

        public override async Task<User> Get(int id)
        {
            return await _dbSet
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _dbSet
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(item => item.Username == name);
        }

        public async Task<List<User>> GetAdmins()
        {
            return await _dbSet
                    .Include(user => user.Roles)
                    .Where(user => user.Roles.Any(item => item.Name == RoleName.Admin))
                    .ToListAsync();
        }

        public async Task<List<User>> GetBasics()
        {
            return await _dbSet
                    .Include(user => user.Roles)
                    .Where(user => user.Roles.Any(item => item.Name == RoleName.Basic))
                    .ToListAsync();
        }
    }
}
