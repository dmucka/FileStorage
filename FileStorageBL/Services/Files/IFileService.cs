using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IFileService
    {
        Task<List<File>> GetAllAsync();
        Task<File> GetById(int id);
    }
}
