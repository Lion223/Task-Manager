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
        /// Add the task
        /// </summary>
        /// <param name="task">Added task</param>
        /// <returns></returns>
        Task<TaskModel> Add(TaskModel task);
        /// <summary>
        /// Find the task by its id
        /// </summary>
        /// <param name="id">id of searched task</param>
        /// <returns></returns>
        Task<TaskModel> Find(int id);
        /// <summary>
        /// Remove the task
        /// </summary>
        /// <param name="task">Task to be removed</param>
        void Remove(TaskModel task);
        /// <summary>
        /// Save all changes done to the database
        /// </summary>
        /// <returns></returns>
        Task SaveChanges();
    }
}
