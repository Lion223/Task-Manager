using System.Data.Entity;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Concrete
{
    /// <summary>
    /// The Entity Framework database context class
    /// <summary>
    public class EFDbContext : DbContext
    {
        /// <summary>
        /// Access to the "TODOs" table
        /// </summary>
        public DbSet<TaskModel> Tasks { get; set; }

        /// <summary>
        /// For some reason EF won't find the connection string, 
        /// even if it's name is the same as this class' name, 
        /// unless it's passed through constructor 
        /// </summary>
        /// <param name="connString">Connection string</param>
        public EFDbContext(string connString) : base(connString) { }
    }
}
