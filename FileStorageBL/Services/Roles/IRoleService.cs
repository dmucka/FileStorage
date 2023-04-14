using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetById(int id);
    }
}
