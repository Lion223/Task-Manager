using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity
{
    /// <summary>
    /// Identity's additional user model class to indicate Id as integer
    /// </summary>
    public class UserModel : IdentityUser<int>
    {
    }
}
