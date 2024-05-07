namespace trialtestr.Models
{
    public class TaskType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for related Tasks
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}