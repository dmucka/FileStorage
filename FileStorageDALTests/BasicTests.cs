using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Repository;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL.Query;

namespace FileStorageDALTests
{

    public class FileStorageInMemoryContext : FileStorageContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FileStorage");
        }
    }

    [TestFixture]
    public class BasicTests
    {
        [Test]
        public async Task AddAndGetFolder()
        {
            List<Folder> folders;
            Folder newFolder, addedFolder;
            int countBefore, countAfter;

            using (var uow = new UnitOfWork(() => new FileStorageInMemoryContext(), new RepositoryFactory(), new QueryFactory()))
            {
                var userRepo = uow.UserRepository;
                var folderRepo = uow.FolderRepository;
                folders = await folderRepo.GetAll();
                countBefore = folders.Count;

                newFolder = new Folder() { Name = "test", Owner = await userRepo.GetByName("admin") };
                await folderRepo.Add(newFolder);

                await uow.SaveAsync();

                folders = await folderRepo.GetAll();
                addedFolder = await folderRepo.Get(newFolder.Id);
                countAfter = folders.Count;
            }

            Assert.AreEqual(countBefore + 1, countAfter);
            Assert.AreEqual(newFolder, addedFolder);
            Assert.AreEqual(newFolder, folders[folders.Count - 1]);
        }

        [Test]
        public async Task UserGetByNameReturnsNull()
        {
            User expectedResult = null;
            User actualResult;
            using (var uow = new UnitOfWork(() => new FileStorageInMemoryContext(), new RepositoryFactory(), new QueryFactory()))
            {
                var userRepo = uow.UserRepository;
                actualResult = await userRepo.GetByName("notExistingName");
            }
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
