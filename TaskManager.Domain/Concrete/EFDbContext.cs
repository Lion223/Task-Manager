using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Concrete
{
    /// <summary>
    /// The Entity Framework database context class
    /// <summary>
    public class EFDbContext : DbContext
    {
        #region Public properties

        /// <summary>
        /// Access to the "TODOs" table
        /// </summary>
        public DbSet<TaskModel> Tasks { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Set the connection string
        /// </summary>
        /// <param name="optionsBuilder">Options to be used by the context</param>
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        #endregion
    }
}
