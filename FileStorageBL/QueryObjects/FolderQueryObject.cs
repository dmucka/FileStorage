using AutoMapper;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.QueryObjects
{
    public class FolderQueryObject : BaseQueryObject
    {
        private readonly FolderQuery _query;
        public FolderQueryObject(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _query = unitOfWork.FolderQuery;
        }

        public void Where(int id)
        { 
            _query.Where(id);
        }

        //public async Task<IEnumerable<FolderDto>> ExecuteAsync(int pageNumber, int pageSize)
        public async Task<IEnumerable<Folder>> ExecuteAsync(int pageNumber, int pageSize)
        {
            _query.OrderByIdAsc();
            _query.Page(pageSize, pageNumber);
            var result = await _query.ExecuteAsync();
            return result;// _mapper.Map<IEnumerable<FolderDto>>(result);
        }
    }
}
