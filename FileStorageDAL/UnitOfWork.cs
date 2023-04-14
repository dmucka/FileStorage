using System;
using System.Threading.Tasks;
using FileStorageDAL.Query;
using FileStorageDAL.Repository;

namespace FileStorageDAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly FileStorageContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IQueryFactory _queryFactory;

        public UnitOfWork(Func<FileStorageContext> contextFactory, IRepositoryFactory repositoryFactory, IQueryFactory queryFactory)
        {
            _context = contextFactory.Invoke();
            _repositoryFactory = repositoryFactory;
            _queryFactory = queryFactory;
        }

        // repositories
        public FileRepository FileRepository => _repositoryFactory.GetFileRepository(_context);

        public FileVersionRepository FileVersionRepository => _repositoryFactory.GetFileVersionRepository(_context);

        public FolderRepository FolderRepository => _repositoryFactory.GetFolderRepository(_context);

        public LogRepository LogRepository => _repositoryFactory.GetLogRepository(_context);

        public RoleRepository RoleRepository => _repositoryFactory.GetRoleRepository(_context);

        public UserRepository UserRepository => _repositoryFactory.GetUserRepository(_context);

        public VersionedFileRepository VersionedFileRepository => _repositoryFactory.GetVersionedFileRepository(_context);

        // queries
        public FileVersionWithFileQuery FileVersionWithFileQuery => _queryFactory.GetFileVersionWithFileQuery(_context);
        public FolderQuery FolderQuery => _queryFactory.GetFolderQuery(_context);
        public LogQuery LogQuery => _queryFactory.GetLogQuery(_context);
        public UserWithFoldersQuery UserWithFoldersQuery => _queryFactory.GetUserWithFoldersQuery(_context);
        public UserWithRolesQuery UserWithRolesQuery => _queryFactory.GetUserWithRolesQuery(_context);
        public VersionedFileQuery VersionedFileQuery => _queryFactory.GetVersionedFileQuery(_context);

        // commit
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
