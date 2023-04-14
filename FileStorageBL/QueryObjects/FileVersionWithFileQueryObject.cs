using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class FileVersionWithFileQueryObject : BaseQueryObject
    {
        private readonly FileVersionWithFileQuery _query;
        public FileVersionWithFileQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.FileVersionWithFileQuery;
        }

        public async Task<IEnumerable<FileVersionDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();
            return _mapper.Map<IEnumerable<FileVersionDto>>(result);
        }
    }
}
