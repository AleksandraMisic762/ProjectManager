using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProjectManager.Models
{
    public class Project
    {
        [Key]
        public int ProjectCode { get; set; }

        public string ProjectName { get; set; }
        public User Manager { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
