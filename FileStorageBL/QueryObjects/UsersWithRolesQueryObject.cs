using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class UsersWithRolesQueryObject : BaseQueryObject
    {
        private readonly UserWithRolesQuery _query;
        public UsersWithRolesQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.UserWithRolesQuery;
        }

        public async Task<IEnumerable<UserShowDto>> ExecuteAsync(string roleName, int pageNumber, int pageSize)
        {
            _query.FilterByRole(roleName);
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();
            return _mapper.Map<IEnumerable<UserShowDto>>(result);
        }
    }
}
