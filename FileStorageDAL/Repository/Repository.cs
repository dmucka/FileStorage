using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageDAL.Repository
{
    public class Repository<T> where T : BaseModel
    {
        internal readonly FileStorageContext _context;
        internal readonly DbSet<T> _dbSet;

        public Repository(FileStorageContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            
            /*if (_dbSet != null)
            {
                _context.Entry(_dbSet).State = EntityState.Detached;
            }
            _context.SaveChanges();*/
        }

        public virtual async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(int entityId)
        {
            var entity = _dbSet.Find(entityId);
            _dbSet.Remove(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            /*
             var foundEntity = _dbSet.Find(entity.Id);
            _dbSet.Update(foundEntity);
             */
            _dbSet.Update(entity);
        }
    }
}
