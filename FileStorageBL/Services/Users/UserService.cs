using FileStorageDAL.Repository;
using FileStorageDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageBL.DTOs;
using AutoMapper;
using FileStorageBL.QueryObjects;
using FileStorageDAL;
using Microsoft.AspNetCore.Identity;

namespace FileStorageBL.Services
{
    public class UserService : CrudQueryBaseService<User, UserCreateDto, UserShowDto, UserUpdateDto>, IUserService
    {
        private readonly UsersWithRolesQueryObject _usersWithRolesQueryObject;
        private readonly UsersWithFoldersQueryObject _usersWithFoldersQueryObject;
        public UserService(UnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.UserRepository)
        {
            _usersWithRolesQueryObject = new UsersWithRolesQueryObject(unitOfWork, mapper);
            _usersWithFoldersQueryObject = new UsersWithFoldersQueryObject(unitOfWork, mapper);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<User> GetByName(string name)
        {
            return await (Repository as UserRepository).GetByName(name);
        }

        public async Task<User> GetById(int id)
        {
            return await (Repository as UserRepository).Get(id);
        }

        public async Task<List<User>> GetAdmins()
        {
            return await (Repository as UserRepository).GetAdmins();
        }

        public async Task<List<User>> GetBasics()
        {
            return await (Repository as UserRepository).GetBasics();
        }

        public async Task<IEnumerable<UserShowDto>> GetUsersWithRolesAsync(string roleName, int pageNumber, int pageSize)
        {
            if (roleName == null)
            {
                roleName = string.Empty;
            }
            return await _usersWithRolesQueryObject.ExecuteAsync(roleName, pageNumber, pageSize);
        }

        public async Task<IEnumerable<UserShowDto>> GetUsersWithFoldersAsync(int pageNumber, int pageSize)
        {
            return await _usersWithFoldersQueryObject.ExecuteAsync(pageNumber, pageSize);
        }

        public async Task<UserShowDto> AuthorizeUserAsync(UserLoginDto login)
        {
            var hasher = new PasswordHasher<User>();
            var user = await GetByName(login.Username);

            if (user != null && hasher.VerifyHashedPassword(null, user.Password, login.Password) == PasswordVerificationResult.Success)
                return _mapper.Map<UserShowDto>(user);

            return null;
        }

        public async Task<User> RegisterUserAsync(UserCreateDto user)
        {
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(null, user.Password);
            var entity = await Create(user);
            return entity;
        }
    }
}
