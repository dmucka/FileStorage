using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class LogQueryObject : BaseQueryObject
    {
        private readonly LogQuery _query;
        public LogQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.LogQuery;
        }

        public async Task<IEnumerable<LogDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();
            return _mapper.Map<IEnumerable<LogDto>>(result);
        }
    }
}
