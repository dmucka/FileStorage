using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL
{
    public class FileStorageContext : DbContext
    {
        private const string _connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=FileStorageDB;Trusted_Connection=True;";

        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FileVersion> FileVersions { get; set; }
        public DbSet<VersionedFile> VersionedFiles { get; set; }

        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<VersionedFileUser> FileVersionsUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            //    .UseSqlServer("Data Source=tcp:filestoragepldbserver.database.windows.net,1433;Initial Catalog=FileStoragePL_db;User Id=filestorage@filestoragepldbserver;Password=Storage123");
                .UseSqlServer(_connectionString);
            //     .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // potrebne pre seeding
            modelBuilder
                .Entity<User>()
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users)
                .UsingEntity<UserRole>(j => j.HasOne(userRole => userRole.Role).WithMany().HasForeignKey(userRole => userRole.RoleId),
                                       j => j.HasOne(userRole => userRole.User).WithMany().HasForeignKey(userRole => userRole.UserId),
                                       j => j.HasKey(userRole => new { userRole.Id, userRole.RoleId, userRole.UserId }));

            // chceme, aby sa pri vymazani usera nevymazali vsetky subory, ktore mu su zdielane (a naopak)
            modelBuilder
                .Entity<VersionedFile>()
                .HasMany(versionedFile => versionedFile.SharedUsers)
                .WithMany(user => user.SharedFiles)
                .UsingEntity<VersionedFileUser>(j => j.HasOne(vfu => vfu.User).WithMany().HasForeignKey(vfu => vfu.UserId).OnDelete(DeleteBehavior.NoAction),
                                                j => j.HasOne(vfu => vfu.VersionedFile).WithMany().HasForeignKey(vfu => vfu.VersionedFileId).OnDelete(DeleteBehavior.NoAction),
                                                j => j.HasKey(vfu => new { vfu.Id, vfu.VersionedFileId, vfu.UserId }));

            // potrebne, pretoze v tejto entite sa nachadzaju 2 zavislosti (N:1 a 1:1)
            modelBuilder
                .Entity<VersionedFile>()
                .HasMany(versionedFile => versionedFile.FileVersions)
                .WithOne(fileVersion => fileVersion.VersionedFile);

            modelBuilder
                .Entity<File>()
                .HasOne(file => file.FileVersion)
                .WithOne(fv => fv.File)
                .HasForeignKey<File>(file => file.FileVersionId);

            // one sided relationship pre VersionedFile
            modelBuilder
                .Entity<VersionedFile>()
                .HasOne(versionedFile => versionedFile.NewestVersion)
                .WithOne()
                .HasForeignKey<VersionedFile>(versionedFile => versionedFile.NewestVersionId)
                .OnDelete(DeleteBehavior.NoAction);

            // one sided relationship pre logy
            modelBuilder
                .Entity<Log>()
                .HasOne(log => log.User)
                .WithOne()
                .HasForeignKey<Log>(log => log.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Log>()
                .HasOne(log => log.File)
                .WithOne()
                .HasForeignKey<Log>(log => log.FileId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
