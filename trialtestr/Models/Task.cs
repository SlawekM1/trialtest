using System;

namespace trialtestr.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; }
        public int TaskTypeId { get; set; }
        public int AssignedToId { get; set; }
        public int CreatorId { get; set; }

        // Corrected navigation properties
        public Project Project { get; set; }   // Instead of 'object', use 'Project'
        public TaskType TaskType { get; set; } // Instead of 'object', use 'TaskType'
        public TeamMember AssignedTo { get; set; } // Ensure this is correct as per your design
        public TeamMember Creator { get; set; } // Ensure this is correct as per your design
    }

}

