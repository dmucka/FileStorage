using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageBL.QueryObjects;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public class LogService : CrudQueryBaseService<Log, LogDto, LogDto, LogDto>, ILogService
    {
        private readonly LogQueryObject _logQueryObject;
        public LogService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.LogRepository)
        {
            _logQueryObject = new LogQueryObject(unitOfWork, mapper);
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<Log> GetById(int id)
        {
            return await (Repository as LogRepository).Get(id);
        }

        public async Task<IEnumerable<LogDto>> GetLogsWithFilesAndUsers(int pageNumber, int pageSize)
        {
            return await _logQueryObject.ExecuteAsync(pageNumber, pageSize);
        }
    }
}
