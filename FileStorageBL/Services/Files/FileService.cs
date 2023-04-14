using FileStorageDAL.Repository;
using FileStorageDAL.Models;
using System.Collections.Generic;
using AutoMapper;
using FileStorageBL.DTOs;
using System.Threading.Tasks;
using FileStorageDAL;

namespace FileStorageBL.Services
{
    public class FileService : CrudQueryBaseService<File, FileDto, FileDto, FileDto>, IFileService
    {

        public FileService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.FileRepository)
        {
        }

        public async Task<List<File>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<File> GetById(int id)
        {
            return await (Repository as FileRepository).Get(id);
        }
    }
}
