using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class RoleFacade : BaseFacade
    {
        private readonly RoleService _roleService;

        public RoleFacade(UnitOfWork unitOfWork, RoleService roleService) : base(unitOfWork)
        {
            _roleService = roleService;
        }

        public async Task<Role> CreateRoleAsync(RoleDto role)
        {
            var entity = await _roleService.Create(role);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateRoleAsync(RoleDto role)
        {
            await _roleService.Update(role);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            _roleService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleService.GetById(id);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleService.GetAllAsync();
        }
    }
}
