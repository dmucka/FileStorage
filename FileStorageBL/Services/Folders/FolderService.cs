using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageBL.QueryObjects;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public class FolderService : CrudQueryBaseService<Folder, FolderCreateDto, FolderShowDto, FolderUpdateDto>, IFolderService
    {
        private readonly FolderQueryObject _folderQueryObject;
        public FolderService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.FolderRepository)
        {
            _folderQueryObject = new FolderQueryObject(unitOfWork, mapper);
        }

        public async Task<List<Folder>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<Folder> GetById(int id)
        {
            return await (Repository as FolderRepository).Get(id);
        }

        public async Task<IEnumerable<Folder>> GetAllByIdAsync(int id, int pageNumber, int pageSize)
        {
            var all = await GetAllAsync();
            _folderQueryObject.Where(id);
            return await _folderQueryObject.ExecuteAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Folder>> GetFoldersWithFoldersAndVersionedFiles(int pageNumber, int pageSize)
        {
            return await _folderQueryObject.ExecuteAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Folder>> GetAllFoldersByUserId(int userId)
        {
            return await (Repository as FolderRepository).GetAllByUserId(userId);
        }

        public async Task<IEnumerable<Folder>> GetAllRootFoldersByUserId(int userId)
        {
            return await (Repository as FolderRepository).GetAllRootByUserId(userId);
        }
    }
}
