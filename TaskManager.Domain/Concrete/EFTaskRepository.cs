using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Domain.Concrete
{
    /// <summary>
    /// Entity Framework implementation of repository for tasks
    /// <summary>

    // Async void methods caused "Cannot access a disposed context instance"
    // TODO: Update is not working
    public class EFTaskRepository : ITaskRepository
    {
        #region Private members

        /// <summary>
        /// EF context
        /// </summary>
        private EFDbContext _context; 

        #endregion

        #region Constructor

        /// <summary>
        /// Receive the context through DI
        /// </summary>
        public EFTaskRepository(EFDbContext context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// Access the tasks table
        /// </summary>
        public IEnumerable<TaskModel> Tasks => _context.Tasks;

        /// <summary>
        /// Add a task to the table
        /// </summary>
        /// <param name="task">Added task</param>
        /// <returns></returns>
        public async Task CreateAsync(TaskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Find the task by id
        /// </summary>
        /// <param name="id">id of the searched task</param>
        /// <returns></returns>
        public async Task<TaskModel> FindAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="task">Updated version of the task</param>
        public async Task UpdateAsync(TaskModel task)
        {
            _context.Tasks.Attach(task);
            var entry = _context.Entry(task);
            entry.State = EntityState.Modified;

            // Microsoft.EntityFrameworkCore.Database.Transaction: Error: An error occurred using a transaction.
            // Exception thrown: 'System.ObjectDisposedException' in System.Private.CoreLib.dll
            await _context.SaveChangesAsync();
            //_context.Update(task);
            //await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the task
        /// </summary>
        /// <param name="task">Deleted task</param>
        public async Task DeleteAsync(TaskModel task)
        {
            _context.Entry(task).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Save changes commited to the task table
        /// Used for external purposes (checked changed, for example)
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
