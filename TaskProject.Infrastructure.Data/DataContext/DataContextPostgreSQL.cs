
using Microsoft.EntityFrameworkCore;
using TaskProject.Infrastructure.Data.Configs;
using TaskProject.Domain.Entities;


namespace TaskProject.Infrastructure.Data.DataContext
{
    internal class DataContextPostgreSQL : DbContext, IDataContext
    {
        public DataContextPostgreSQL()
        {

        }


        public DataContextPostgreSQL(DbContextOptions<DataContextPostgreSQL> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=66.70.189.199; Database=TaskProject; user id= pacto_ltd ; password = pacto#2022@;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskConfig());
            modelBuilder.ApplyConfiguration(new TaskStatusConfig());
            modelBuilder.ApplyConfiguration(new TaskStatusLogConfig());
        }


        public virtual DbSet<Domain.Entities.Task> Tasks { get; set; } = null!;
        public virtual DbSet<Domain.Entities.TaskStatus> TaskStatuses { get; set; } = null!;
        public virtual DbSet<TaskStatusLog> TaskStatusLogs { get; set; } = null!;
    }
}