using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class LogFacade : BaseFacade
    {
        private readonly LogService _logService;

        public LogFacade(UnitOfWork unitOfWork, LogService logService) : base(unitOfWork)
        {
            _logService = logService;
        }

        public async Task<Log> CreateLogAsync(LogDto log)
        {
            var entity = await _logService.Create(log);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateLogAsync(LogDto log)
        {
            await _logService.Update(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteLogAsync(int id)
        {
            _logService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Log> GetLogByIdAsync(int id)
        {
            return await _logService.GetById(id);
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _logService.GetAllAsync();
        }

        public async Task<IEnumerable<LogDto>> GetLogsWithFilesAndUsersAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _logService.GetLogsWithFilesAndUsers(pageNumber, pageSize);
        }
    }
}
