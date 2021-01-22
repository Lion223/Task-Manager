using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    /// <summary>
    /// Domain model for a task
    /// <summary>
    // Written the attribute with full name, because just "Table(...)" wasn't working, don't remember, it may work, but don't want to change anything that works at the moment
    [System.ComponentModel.DataAnnotations.Schema.Table("Tasks")]
    // Metadata or Buddy class to separate data annotation logic into it's own class
        // https://stackoverflow.com/questions/4798138/validation-on-my-domain-objects-or-in-the-view-model-where-should-it-go
        // https://stackoverflow.com/questions/38371519/what-exactly-is-a-buddy-class-and-how-do-i-use-it-to-add-annotations-to-an-exist
    [MetadataType(typeof(TaskModelMetadata))]
    public partial class TaskModel
    {
    }

    public class TaskModelMetadata
    {
        [ScaffoldColumn(false)]
        [Key]
        public int TaskId { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Select the type")]
        public string Type { get; set; }
        [Required]
        [Range(0, 3, ErrorMessage = "Select priority on a scale of 0 to 3")]

        // Nullable if need to know that user hasn't selected a value
        // So that "Required" attribute will report a validation error (Pro ASP.NET MVC 5, p.37)
        // That also fixes the issue, when SelectTagHelper puts "selected" attribute on int's default value option which is 0
        // Instead of making placeholder selected by default
        public int? Priority { get; set; }
        [Required(ErrorMessage = "Select the date for the deadline")]
        public DateTime? Deadline { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write the description of the task")]
        public string Description { get; set; }

        // Non-nullable for the reason, that by default the task in unfinished, which is intentional
        [ScaffoldColumn(false)]
        [Required]
        public bool Finished { get; set; }
    }
}
