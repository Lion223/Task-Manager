using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract
{
    /// <summary>
    /// Interface of repository for tasks
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Get the list of tasks
        /// </summary>
        IEnumerable<TaskModel> Tasks { get; }

        /// <summary>
        /// Create the task
        /// </summary>
        /// <param name="task">Created task</param>
        /// <returns></returns>
        Task CreateAsync(TaskModel task);

        /// <summary>
        /// Find the task by its id
        /// </summary>
        /// <param name="id">id of searched task</param>
        /// <returns></returns>
        Task<TaskModel> FindAsync(int id);

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="task">Updated task</param>
        Task UpdateAsync(TaskModel task);

        /// <summary>
        /// Deletes the task
        /// </summary>
        /// <param name="task">Task to be deleted</param>
        Task DeleteAsync(TaskModel task);

        /// <summary>
        /// Save all changes done to the database
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
