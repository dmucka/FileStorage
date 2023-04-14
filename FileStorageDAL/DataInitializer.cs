using FileStorageDAL.Enums;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Name = RoleName.Basic }, 
                                                new Role() { Id = 2, Name = RoleName.Admin });

            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Username = "admin", Email = "admin@test.com", Password = hasher.HashPassword(null, "admin") });

            modelBuilder.Entity<UserRole>().HasData(new UserRole() { Id = 1, UserId = 1, RoleId = 1 }, 
                                                    new UserRole() { Id = 2, UserId = 1, RoleId = 2 });
        }
    }
}
