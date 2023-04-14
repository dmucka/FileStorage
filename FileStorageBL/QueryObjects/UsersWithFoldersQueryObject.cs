using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class UsersWithFoldersQueryObject : BaseQueryObject
    {
        private readonly UserWithFoldersQuery _query;
        public UsersWithFoldersQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.UserWithFoldersQuery;
        }

        public async Task<IEnumerable<UserShowDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();
            return _mapper.Map<IEnumerable<UserShowDto>>(result);
        }
    }
}
