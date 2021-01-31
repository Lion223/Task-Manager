using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity
{
    /// <summary>
    /// Identity's additional role model class to indicate Id as integer
    /// </summary>
    public class RoleModel : IdentityRole<int>
    {
    }
}
