using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebUI.ViewModels.Tasks
{
    public class CreateViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Select the type")]
        public string Type { get; set; }
        [Required]
        [Range(0, 3, ErrorMessage = "Select priority on a scale of 0 to 3")]
        public int? Priority { get; set; }
        [Required(ErrorMessage = "Select the date for the deadline")]
        public DateTime? Deadline { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write the description of the task")]
        public string Description { get; set; }
    }
}
