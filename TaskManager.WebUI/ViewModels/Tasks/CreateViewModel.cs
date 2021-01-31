using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebUI.ViewModels.Tasks
{
    /// <summary>
    /// A view model for the create operation
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// Task's type 
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = false, ErrorMessage = "Select the type")]
        public string Type { get; set; }

        /// <summary>
        /// Task's priority 
        /// </summary>
        [Required]
        [Range(0, 3, ErrorMessage = "Select priority on a scale of 0 to 3")]
        public int? Priority { get; set; }

        /// <summary>
        /// Task's deadline 
        /// </summary>
        [Required(ErrorMessage = "Select the date for the deadline")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Task's description 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write the description of the task")]
        public string Description { get; set; }
    }
}
