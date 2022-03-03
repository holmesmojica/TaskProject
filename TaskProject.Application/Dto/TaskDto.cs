
namespace TaskProject.Application.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int TaskStatusId { get; set; }

        public string FormattedCreationDate
        {
            get
            {
                return CreationDate.ToString("dd MMM, yyyy hh:mm tt");
            }
        }
    }
}