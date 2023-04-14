using Microsoft.AspNetCore.Authorization;
using FileStorageDAL.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FileStoragePL
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }

    public class RequireBasicAttribute : AuthorizeRolesAttribute
    {
        public RequireBasicAttribute() : base(RoleName.Basic, RoleName.Admin)
        {
        }
    }

    public class RequireAdminAttribute : AuthorizeRolesAttribute
    {
        public RequireAdminAttribute() : base(RoleName.Admin)
        {
        }
    }

    public static class AuthorizationExtension
    {
        public static bool IsUserLogged(this PageModel model)
        {
            return model.User.Identity.IsAuthenticated;
        }

        public static bool IsUserAdmin(this PageModel model)
        {
            return model.User.HasClaim(ClaimTypes.Role, RoleName.Admin);
        }
    }
}
