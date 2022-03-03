
namespace TaskProject.Domain.Entities
{
    public class Task
    {
        public Task()
        {
            TaskStatusLogs = new HashSet<TaskStatusLog>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public virtual ICollection<TaskStatusLog> TaskStatusLogs { get; set; }
    }
}