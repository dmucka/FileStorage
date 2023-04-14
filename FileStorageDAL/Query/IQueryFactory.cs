namespace FileStorageDAL.Query
{
    public interface IQueryFactory
    {
        FileVersionWithFileQuery GetFileVersionWithFileQuery(FileStorageContext context);
        FolderQuery GetFolderQuery(FileStorageContext context);
        LogQuery GetLogQuery(FileStorageContext context);
        UserWithFoldersQuery GetUserWithFoldersQuery(FileStorageContext context);
        UserWithRolesQuery GetUserWithRolesQuery(FileStorageContext context);
        VersionedFileQuery GetVersionedFileQuery(FileStorageContext context);
    }
}
