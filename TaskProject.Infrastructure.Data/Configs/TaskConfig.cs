
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskProject.Infrastructure.Data.Configs
{
    internal class TaskConfig : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("task");

            builder.Property(task => task.Id)
                .HasColumnName("id");

            builder.Property(task => task.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");

            builder.Property(task => task.Description)
                .HasColumnName("description");

            builder.Property(task => task.Title)
                .HasMaxLength(120)
                .HasColumnName("title");
        }
    }
}