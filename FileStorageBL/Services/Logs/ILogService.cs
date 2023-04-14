using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public interface ILogService
    {
        Task<List<Log>> GetAllAsync();
        Task<Log> GetById(int id);

        Task<IEnumerable<LogDto>> GetLogsWithFilesAndUsers(int pageNumber, int pageSize);
    }
}
