using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    public class ProjectTasksViewModel
    {
        public Models.Project Project { get; set; }
        public IEnumerable<Models.Task> Tasks { get; set; }
        public int ProjectProgress { get; set; }
    }
}
