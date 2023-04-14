using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class FileFacade : BaseFacade
    {
        private readonly FileService _fileService;
        private readonly FolderService _folderService;

        public FileFacade(UnitOfWork unitOfWork, FileService fileService, FolderService folderService) : base(unitOfWork)
        {
            _fileService = fileService;
            _folderService = folderService;
        }

        public async Task<File> CreateFileAsync(FileDto file)
        {
            var entity = await _fileService.Create(file);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateFileAsync(FileDto file)
        {
            await _fileService.Update(file);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFileAsync(int id)
        {
            _fileService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<File> GetFileByIdAsync(int id)
        {
            return await _fileService.GetById(id);
        }

        public async Task<List<File>> GetAllFilesAsync()
        {
            return await _fileService.GetAllAsync();
        }

        public async Task<User> GetOwner(int id)
        {
            var file = await _fileService.GetById(id);
            var folderId = file.FileVersion.VersionedFile.FolderId;
            var folder = await _folderService.GetById(folderId);

            return folder.Owner;
        }
    }
}
