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
        Task<TaskModel> Create(TaskModel task);

        /// <summary>
        /// Find the task by its id
        /// </summary>
        /// <param name="id">id of searched task</param>
        /// <returns></returns>
        Task<TaskModel> Find(int id);

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="task">Updated task</param>
        void Update(TaskModel task);

        /// <summary>
        /// Deletes the task
        /// </summary>
        /// <param name="task">Task to be deleted</param>
        void Delete(TaskModel task);

        /// <summary>
        /// Save all changes done to the database
        /// </summary>
        /// <returns></returns>
        Task SaveChanges();
    }
}
