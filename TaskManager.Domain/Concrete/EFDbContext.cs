using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using System.Configuration;

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
        /// Set the connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString);
        //}
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }
    }
}
