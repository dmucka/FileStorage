using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IFileVersionService
    {
        Task<List<FileVersion>> GetAllAsync();
        Task<FileVersion> GetById(int id);

        Task<IEnumerable<FileVersionDto>> GetFileVersionsWithFiles(int pageNumber, int pageSize);
    }
}
