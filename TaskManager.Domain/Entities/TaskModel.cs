using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    /// <summary>
    /// Domain model for a task
    /// <summary>
    public class TaskModel
    {
        [ScaffoldColumn(false)]
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool Finished { get; set; }
    }
}
