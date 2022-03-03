
namespace TaskProject.Domain.Entities
{
    public class TaskStatusLog
    {
        public int Id { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public int TaskId { get; set; }
        public int TaskStatusId { get; set; }

        public virtual Task Task { get; set; } = null!;
        public virtual TaskStatus TaskStatus { get; set; } = null!;
    }
}