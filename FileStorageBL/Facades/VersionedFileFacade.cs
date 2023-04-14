using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class VersionedFileFacade : BaseFacade
    {
        private readonly VersionedFileService _versionedFileService;
        private readonly FolderService _folderService;

        public VersionedFileFacade(UnitOfWork unitOfWork, VersionedFileService versionedFileService, FolderService folderService) : base(unitOfWork)
        {
            _versionedFileService = versionedFileService;
            _folderService = folderService;
        }

        public async Task<VersionedFile> CreateVersionedFileAsync(VersionedFileCreateDto file)
        {
            var entity = await _versionedFileService.Create(file);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateVersionedFileAsync(VersionedFileUpdateDto file)
        {
            await _versionedFileService.Update(file);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteVersionedFileAsync(int id)
        {
            _versionedFileService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<VersionedFile> GetVersionedFileByIdAsync(int id)
        {
            return await _versionedFileService.GetById(id);
        }

        public async Task<List<VersionedFile>> GetAllVersionedFilesAsync()
        {
            return await _versionedFileService.GetAllAsync();
        }

        public async Task<User> GetOwner(int id)
        {
            var versionedFile = await _versionedFileService.GetById(id);
            var folderId = versionedFile.FolderId;
            var folder = await _folderService.GetById(folderId);

            return folder.Owner;
        }
    }
}
