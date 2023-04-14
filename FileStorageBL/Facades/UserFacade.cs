using FileStorageBL.DTOs;
using FileStorageBL.Services;
using FileStorageDAL;
using FileStorageDAL.Enums;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageBL.Facades
{
    public class UserFacade : BaseFacade
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public UserFacade(UnitOfWork unitOfWork, UserService userService, RoleService roleService) : base(unitOfWork)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<User> CreateUserAsync(UserCreateDto user)
        {
            var entity = await _userService.Create(user);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task UpdateUserAsync(UserUpdateDto user)
        {
            await _userService.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            _userService.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        // TODO UserShowDto
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _userService.GetByName(name);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userService.GetById(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userService.GetAllAsync();
        }

        public async Task<List<User>> GetAdminsAsync()
        {
            return await _userService.GetAdmins();
        }

        public async Task<List<User>> GetBasicsAsync()
        {
            return await _userService.GetBasics();
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await GetUserByNameAsync(userName) != null;
        }

        public async Task<IEnumerable<UserShowDto>> GetUsersWithRolesAsync(string roleName, int pageNumber = 1, int pageSize = 20)
        {
            return await _userService.GetUsersWithRolesAsync(roleName, pageNumber, pageSize);
        }

        public async Task<IEnumerable<UserShowDto>> GetUsersWithFoldersAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _userService.GetUsersWithFoldersAsync(pageNumber, pageSize);
        }

        public async Task<User> RegisterUserAsync(UserCreateDto user)
        {
            var basicRole = await _roleService.GetByName(RoleName.Basic);
            user.Roles.Add(basicRole);
            var newUser = await _userService.RegisterUserAsync(user);
            await _unitOfWork.SaveAsync();
            return newUser;
        }

        public async Task<UserShowDto> AuthorizeUserAsync(UserLoginDto login)
        {
            return await _userService.AuthorizeUserAsync(login);
        }
    }
}
