
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskProject.Domain.Entities;


namespace TaskProject.Infrastructure.Data.Configs
{
    internal class TaskStatusLogConfig : IEntityTypeConfiguration<TaskStatusLog>
    {
        public void Configure(EntityTypeBuilder<TaskStatusLog> builder)
        {
            builder.ToTable("taskstatuslog");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.StatusChangeDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("statuschangedate");

            builder.Property(e => e.TaskId)
                .HasColumnName("taskid");

            builder.Property(e => e.TaskStatusId)
                .HasColumnName("taskstatusid");

            builder.HasOne(d => d.Task)
                .WithMany(p => p.TaskStatusLogs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("taskstatuslog_taskid_fkey");

            builder.HasOne(d => d.TaskStatus)
                .WithMany(p => p.TaskStatusLogs)
                .HasForeignKey(d => d.TaskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("taskstatuslog_taskstatusid_fkey");
        }
    }
}