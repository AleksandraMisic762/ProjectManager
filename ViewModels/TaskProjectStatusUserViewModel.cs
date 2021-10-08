using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    public class TaskProjectStatusUserViewModel
    {

        public Models.Task Task { get; set; }
        public IEnumerable<Models.Project> Projects { get; set; }
        public IEnumerable<string> Statuses { get; set; }
        public IEnumerable<Models.User> Users { get; set; }
    }
}
