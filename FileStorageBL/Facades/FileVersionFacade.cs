using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class FileVersionFacade : BaseFacade
    {
        private readonly FileVersionService _fileVersionService;
        private readonly FolderService _folderService;

        public FileVersionFacade(UnitOfWork unitOfWork, FileVersionService fileVersionService, FolderService folderService) : base(unitOfWork)
        {
            _fileVersionService = fileVersionService;
            _folderService = folderService;
        }

        public async Task<FileVersion> CreateFileVersionAsync(FileVersionDto file)
        {
            var entity = await _fileVersionService.Create(file);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateFileVersionAsync(FileVersionDto file)
        {
            await _fileVersionService.Update(file);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFileVersionAsync(int id)
        {
            _fileVersionService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<FileVersion> GetFileVersionByIdAsync(int id)
        {
            return await _fileVersionService.GetById(id);
        }

        public async Task<FileVersion> GetFileVersionByFileIdAsync(int id)
        {
            return await _fileVersionService.GetByFileId(id);
        }

        public async Task<List<FileVersion>> GetAllFileVersionsAsync()
        {
            return await _fileVersionService.GetAllAsync();
        }

        public async Task<IEnumerable<FileVersionDto>> GetFileVersionsWithFilesAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _fileVersionService.GetFileVersionsWithFiles(pageNumber, pageSize);
        }

        public async Task<User> GetOwner(int id)
        {
            var fileVersion = await _fileVersionService.GetById(id);
            var folderId = fileVersion.VersionedFile.FolderId;
            var folder = await _folderService.GetById(folderId);

            return folder.Owner;
        }
    }
}
