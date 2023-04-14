using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public abstract class Query<T> where T : BaseModel
    {
        protected IQueryable<T> _query;

        public Query(FileStorageContext context)
        {
            _query = context.Set<T>();
        }

        public async Task<IEnumerable<T>> ExecuteAsync()
        {
            return await _query?.ToListAsync() ?? new List<T>();
        }

        /// <summary>
        /// Page starts with 1.
        /// </summary>
        public void Page(int pageSize, int pageNumber)
        {
            _query = _query.Skip(pageSize * (pageNumber - 1))
                           .Take(pageSize);
        }

        public void OrderByIdAsc()
        {
            _query = _query.OrderBy(x => x.Id);
        }

        public void OrderByIdDesc()
        {
            _query =  _query.OrderByDescending(x => x.Id);
        }

        
    }
}
