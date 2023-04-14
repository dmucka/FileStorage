namespace FileStorageDAL.Query
{
    public class QueryFactory : IQueryFactory
    {
        public QueryFactory()
        {

        }

        public FileVersionWithFileQuery GetFileVersionWithFileQuery(FileStorageContext context) => new FileVersionWithFileQuery(context);

        public FolderQuery GetFolderQuery(FileStorageContext context) => new FolderQuery(context);

        public LogQuery GetLogQuery(FileStorageContext context) => new LogQuery(context);

        public UserWithFoldersQuery GetUserWithFoldersQuery(FileStorageContext context) => new UserWithFoldersQuery(context);

        public UserWithRolesQuery GetUserWithRolesQuery(FileStorageContext context) => new UserWithRolesQuery(context);

        public VersionedFileQuery GetVersionedFileQuery(FileStorageContext context) => new VersionedFileQuery(context);
    }
}
