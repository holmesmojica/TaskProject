
using Microsoft.EntityFrameworkCore;
using TaskProject.Domain.Entities;


namespace TaskProject.Infrastructure.Data.DataContext
{
    internal interface IDataContext
    {
        DbSet<Domain.Entities.Task> Tasks { get; set; }
        DbSet<Domain.Entities.TaskStatus> TaskStatuses { get; set; }
        DbSet<TaskStatusLog> TaskStatusLogs { get; set; }
    }
}