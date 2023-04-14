using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class FolderFacade : BaseFacade
    {
        private readonly FolderService _folderService;

        public FolderFacade(UnitOfWork unitOfWork, FolderService folderService) : base(unitOfWork)
        {
            _folderService = folderService;
        }

        public async Task<Folder> CreateFolderAsync(FolderCreateDto folder)
        {
            var entity = await _folderService.Create(folder);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateFolderAsync(FolderUpdateDto folder)
        {
            await _folderService.Update(folder);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFolderAsync(int id)
        {
            _folderService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Folder> GetFolderByIdAsync(int id)
        {
            return await _folderService.GetById(id);
        }

        public async Task<List<Folder>> GetAllFoldersAsync()
        {
            return await _folderService.GetAllAsync();
        }

        public async Task<List<Folder>> GetAllFoldersByIdAsync(int id, int pageNumber = 1, int pageSize = 20)
        {
            return (List<Folder>)await _folderService.GetAllByIdAsync(id, pageNumber, pageSize);
        }

        public async Task<List<Folder>> GetFoldersWithFoldersAndVersionedFilesAsync(int pageNumber = 1, int pageSize = 20)
        {
            return (List<Folder>)await _folderService.GetFoldersWithFoldersAndVersionedFiles(pageNumber, pageSize);
        }

        public async Task<List<Folder>> GetAllFoldersByUserId(int userId)
        {
            return (List<Folder>)await _folderService.GetAllFoldersByUserId(userId);
        }

        public async Task<List<Folder>> GetAllRootFoldersByUserId(int userId)
        {
            return (List<Folder>)await _folderService.GetAllRootFoldersByUserId(userId);
        }
    }
}
