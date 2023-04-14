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
    public class VersionedFileService : CrudQueryBaseService<VersionedFile, VersionedFileCreateDto, VersionedFileShowDto, VersionedFileUpdateDto>, IVersionedFileService
    {
        private readonly VersionedFileQueryObject _versionedFileQueryObject;
        public VersionedFileService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.VersionedFileRepository)
        {
            _versionedFileQueryObject = new VersionedFileQueryObject(unitOfWork, mapper);
        }

        public async Task<List<VersionedFile>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<VersionedFile> GetById(int id)
        {
            return await (Repository as VersionedFileRepository).Get(id);
        }

        public async Task<IEnumerable<VersionedFileShowDto>> GetVersionedFilesWithFileVersions(int pageNumber, int pageSize)
        {
            return await _versionedFileQueryObject.ExecuteAsync(pageNumber, pageSize);
        }
    }
}
