namespace FileStorageDAL.Repository
{
    public interface IRepositoryFactory
    {
        FileRepository GetFileRepository(FileStorageContext context);
        FileVersionRepository GetFileVersionRepository(FileStorageContext context);
        FolderRepository GetFolderRepository(FileStorageContext context);
        LogRepository GetLogRepository(FileStorageContext context);
        RoleRepository GetRoleRepository(FileStorageContext context);
        UserRepository GetUserRepository(FileStorageContext context);
        VersionedFileRepository GetVersionedFileRepository(FileStorageContext context);
    }
}
