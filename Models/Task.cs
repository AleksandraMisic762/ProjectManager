using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Task
    {
        public Project Project { get; set; }
        public User Assignee { get; set; }
        public int TaskId { get; set; }
        public Status Status { get; set; }
        public int Progress { get; set; }
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
