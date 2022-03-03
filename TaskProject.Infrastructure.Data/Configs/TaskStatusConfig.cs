
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskProject.Infrastructure.Data.Configs
{
    internal class TaskStatusConfig : IEntityTypeConfiguration<Domain.Entities.TaskStatus>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskStatus> builder)
        {
            builder.ToTable("taskstatus");

            builder.Property(taskStatus => taskStatus.Id)
                .HasColumnName("id");

            builder.Property(taskStatus => taskStatus.Description)
                .HasMaxLength(15)
                .HasColumnName("description");
        }
    }
}