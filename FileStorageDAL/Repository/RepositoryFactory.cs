namespace FileStorageDAL.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public RepositoryFactory()
        {
        }

        public FileRepository GetFileRepository(FileStorageContext context) => new FileRepository(context);

        public FileVersionRepository GetFileVersionRepository(FileStorageContext context) => new FileVersionRepository(context);

        public FolderRepository GetFolderRepository(FileStorageContext context) => new FolderRepository(context);

        public LogRepository GetLogRepository(FileStorageContext context) => new LogRepository(context);

        public RoleRepository GetRoleRepository(FileStorageContext context) => new RoleRepository(context);

        public UserRepository GetUserRepository(FileStorageContext context) => new UserRepository(context);

        public VersionedFileRepository GetVersionedFileRepository(FileStorageContext context) => new VersionedFileRepository(context);
    }
}
