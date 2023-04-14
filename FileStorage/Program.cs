using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Query;
using FileStorageDAL.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            List<Folder> folders;

            using (var uow = new UnitOfWork(() => new FileStorageContext(), new RepositoryFactory(), new QueryFactory()))
            {
                var userRepo = uow.UserRepository;
                var folderRepo = uow.FolderRepository;

                await folderRepo.Add(new Folder() { Name = "test", Owner = await userRepo.GetByName("admin") });

                await uow.SaveAsync();

                folders = await folderRepo.GetAll();
            }

            foreach (var v in folders)
            {
                Console.WriteLine($"Name: {v.Name}\nOwner: {v.Owner.Username}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}