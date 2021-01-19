using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Domain.Concrete
{
    public class IdentityAppContext : IdentityDbContext<UserModel, RoleModel, int>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {
        }
    }
}
