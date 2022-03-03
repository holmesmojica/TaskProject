
using TaskProject.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskProject.Domain.Enums;

namespace TaskProject.Infrastructure.Data.Repository
{
    public class TaskRepository : IRepository<Domain.Entities.Task>
    {
        private readonly DataContext.DataContextPostgreSQL _dbContext;


        internal TaskRepository(IDataContext dbContext)
        {
            _dbContext = (DataContext.DataContextPostgreSQL)dbContext;
        }


        public void Create(Domain.Entities.Task taskEntity)
        {
            try
            {
                _dbContext.Tasks.Add(taskEntity);
            }
            catch (ArgumentNullException argumentNullException)
            {
                argumentNullException.Source = nameof(taskEntity);
                throw argumentNullException;
            }
        }


        public void Delete(Domain.Entities.Task? task)
        {
            Domain.Entities.Task? selectedTask = GetById(task.Id);

            if (selectedTask == null)
                return;

            _dbContext.Tasks.Remove(selectedTask);
        }


        //Solo carga las tareas que no están en estado DELETED
        public List<Domain.Entities.Task> GetAll()
        {
            try
            {
                return _dbContext
                    .Tasks
                    .Include(task => task.TaskStatusLogs)
                    .Where(task => task.TaskStatusLogs != null && !task.TaskStatusLogs.Any(sl => sl.TaskStatusId == (int)TaskStatusEnum.DELETED))
                    .OrderByDescending(task => task.CreationDate)
                    .ToList();
            }
            catch(NullReferenceException)
            {
                return new List<Domain.Entities.Task>();
            }
        }


        public Domain.Entities.Task GetById(int taskId)
        {
            Domain.Entities.Task? selectedTask = _dbContext.Tasks
                .Include(task => task.TaskStatusLogs)
                .Where(t => t.Id == taskId)
                .FirstOrDefault();

            if (selectedTask == null)
                return new Domain.Entities.Task();

            return selectedTask;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        public void Update(Domain.Entities.Task taskEntity)
        {
            Domain.Entities.Task? selectedTask = _dbContext.Tasks.Where(t => t.Id == taskEntity.Id).FirstOrDefault();

            if (selectedTask == null)
                return;

            selectedTask.Title = taskEntity.Title;
            selectedTask.Description = taskEntity.Description;

            _dbContext.Entry(selectedTask).State = EntityState.Modified;
        }
    }
}