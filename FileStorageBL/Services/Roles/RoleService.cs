using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageDAL.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public class RoleService : CrudQueryBaseService<Role, RoleDto, RoleDto, RoleDto>, IRoleService
    {
        public RoleService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.RoleRepository)
        {
        }

        public async Task<Role> GetByName(string name)
        {
            return await (Repository as RoleRepository).GetByName(name);
        }

        public async Task<Role> GetById(int id)
        {
            return await (Repository as RoleRepository).Get(id);
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await Repository.GetAll();
        }
    }
}
