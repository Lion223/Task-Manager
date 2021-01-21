using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.WebUI.ViewModels.Tasks
{
    public class CreateViewModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public int? Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public string Description { get; set; }
    }
}
