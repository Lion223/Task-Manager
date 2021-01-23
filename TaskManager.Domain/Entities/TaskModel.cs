using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Domain.Entities
{
    /// <summary>
    /// Domain model for a task
    /// <summary>
    // Written the attribute with full name, because just "Table(...)" wasn't working, don't remember, it may work, but don't want to change anything that works at the moment
    [System.ComponentModel.DataAnnotations.Schema.Table("Tasks")]
    public class TaskModel
    {
        [Key]
        public int? TaskId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string Type { get; set; }

        // Nullable if need to know that user hasn't selected a value
        // So that "Required" attribute will report a validation error (Pro ASP.NET MVC 5, p.37)
            // That also fixes the issue, when SelectTagHelper puts "selected" attribute on int's default value option which is 0
            // Instead of making placeholder selected by default
        [Required]
        public int? Priority { get; set; }
        [Required]
        public DateTime? Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Finished { get; set; }
    }
}
