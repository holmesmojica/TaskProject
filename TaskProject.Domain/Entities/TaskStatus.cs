
namespace TaskProject.Domain.Entities
{
    public class TaskStatus
    {
        public TaskStatus()
        {
            TaskStatusLogs = new HashSet<TaskStatusLog>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<TaskStatusLog> TaskStatusLogs { get; set; }
    }
}