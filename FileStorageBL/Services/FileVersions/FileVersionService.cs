using FileStorageDAL.Repository;
using FileStorageDAL.Models;
using System.Collections.Generic;
using AutoMapper;
using FileStorageBL.DTOs;
using System.Threading.Tasks;
using FileStorageBL.QueryObjects;
using FileStorageDAL;

namespace FileStorageBL.Services
{
    public class FileVersionService : CrudQueryBaseService<FileVersion, FileVersionDto, FileVersionDto, FileVersionDto>, IFileVersionService
    {
        private readonly FileVersionWithFileQueryObject _fileVersionWithFileQueryObject;
        public FileVersionService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.FileVersionRepository)
        {
            _fileVersionWithFileQueryObject = new FileVersionWithFileQueryObject(unitOfWork, mapper);
        }

        public async Task<List<FileVersion>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<FileVersion> GetById(int id)
        {
            return await (Repository as FileVersionRepository).Get(id);
        }

        public async Task<FileVersion> GetByFileId(int id)
        {
            return await (Repository as FileVersionRepository).GetByFileId(id);
        }

        public async Task<IEnumerable<FileVersionDto>> GetFileVersionsWithFiles(int pageNumber, int pageSize)
        {
            return await _fileVersionWithFileQueryObject.ExecuteAsync(pageNumber, pageSize);
        }
    }
}
