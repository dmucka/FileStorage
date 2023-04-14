using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByName(string name);
        Task<User> GetById(int id);
        Task<List<User>> GetAdmins();
        Task<List<User>> GetBasics();

        Task<IEnumerable<UserShowDto>> GetUsersWithRolesAsync(string roleName, int pageNumber, int pageSize);
        Task<IEnumerable<UserShowDto>> GetUsersWithFoldersAsync(int pageNumber, int pageSize);
    }
}
