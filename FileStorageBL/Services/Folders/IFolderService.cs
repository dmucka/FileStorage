using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IFolderService
    {
        Task<List<Folder>> GetAllAsync();
        Task<Folder> GetById(int id);

        Task<IEnumerable<Folder>> GetFoldersWithFoldersAndVersionedFiles(int pageNumber, int pageSize);
    }
}
