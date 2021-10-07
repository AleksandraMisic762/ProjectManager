using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private decimal _progress;
        
        public DateTime Deadline { get; set; }
        public string Description { get; set; }

        public decimal Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value >= 0 && value.CompareTo(100.00) < 0)
                {
                    _progress = value;
                }
            }
        }

    }
}
