using System;
using System.Collections.Generic;

namespace trialtestr.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }

        // Navigation property for related Tasks
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}