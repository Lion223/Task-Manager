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
        #region Public properties

        /// <summary>
        /// Get the list of <see cref="TaskModel"/> tasks
        /// </summary>
        IEnumerable<TaskModel> Tasks { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Create the <see cref="TaskModel"/> task
        /// </summary>
        /// <param name="task">Created task</param>
        /// <returns></returns>
        Task CreateAsync(TaskModel task);

        /// <summary>
        /// Find the <see cref="TaskModel"/> task by its id
        /// </summary>
        /// <param name="id">id of searched task</param>
        /// <returns></returns>
        Task<TaskModel> FindAsync(int id);

        /// <summary>
        /// Update the <see cref="TaskModel"/> task
        /// </summary>
        /// <param name="task">Updated task</param>
        Task UpdateAsync(TaskModel task);

        /// <summary>
        /// Deletes the <see cref="TaskModel"/> task
        /// </summary>
        /// <param name="task">Task to be deleted</param>
        Task DeleteAsync(TaskModel task);

        /// <summary>
        /// Save all changes done to the database
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        #endregion
    }
}
