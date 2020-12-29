﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Concrete
{
    /// <summary>
    /// Entity Framework implementation of repository for tasks
    /// <summary>
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
        /// Create the context by passing a connection string
        /// </summary>
        /// <param name="connString">Connection string</param>
        public EFTaskRepository(string connString)
        {
            _context = new EFDbContext(connString);
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
        public async Task<TaskModel> Add(TaskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        /// <summary>
        /// Find the task by id
        /// </summary>
        /// <param name="id">id of the searched task</param>
        /// <returns></returns>
        public async Task<TaskModel> Find(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }
        public void Remove(TaskModel task)
        {
            _context.Entry(task).State = System.Data.Entity.EntityState.Deleted;
        }
        /// <summary>
        /// Save changes commited to the task table
        /// </summary>
        /// <returns></returns>
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
