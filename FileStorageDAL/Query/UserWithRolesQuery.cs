using System.Linq;
using FileStorageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageDAL.Query
{
    public class UserWithRolesQuery : Query<User>
    {
        public UserWithRolesQuery(FileStorageContext context) : base(context)
        {
            _query = _query.Include(user => user.Roles);
        }

        public void FilterByRole(string roleName)
        {
            _query = _query.Where(user => user.Roles.Any(role => role.Name == roleName));
        }
    }
}
