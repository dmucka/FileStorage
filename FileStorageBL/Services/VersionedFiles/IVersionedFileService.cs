using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface IVersionedFileService
    {
        Task<List<VersionedFile>> GetAllAsync();
        Task<VersionedFile> GetById(int id);

        Task<IEnumerable<VersionedFileShowDto>> GetVersionedFilesWithFileVersions(int pageNumber, int pageSize);
    }
}
