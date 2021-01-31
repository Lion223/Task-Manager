using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Domain.Concrete
{
    /// <summary>
    /// Identity's additional context class that uses both additional user and role classes
    /// </summary>
    public class IdentityAppContext : IdentityDbContext<UserModel, RoleModel, int>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {
        }
    }
}
