using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class VersionedFileQueryObject : BaseQueryObject
    {
        private readonly VersionedFileQuery _query;

        public VersionedFileQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.VersionedFileQuery;
        }

        public async Task<IEnumerable<VersionedFileShowDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();

            return _mapper.Map<IEnumerable<VersionedFileShowDto>>(result);
        }
    }
}
