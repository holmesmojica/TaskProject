
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskProject.Infrastructure.Data.DataContext;
using TaskProject.Domain.Entities;


namespace TaskProject.Infrastructure.Data.Repository
{
    public class TaskStatusLogRepository : IRepository<TaskStatusLog>
    {
        private readonly DataContext.DataContextPostgreSQL _dbContext;


        internal TaskStatusLogRepository(IDataContext dbContext)
        {
            _dbContext = (DataContext.DataContextPostgreSQL)dbContext;
        }


        public void Create(TaskStatusLog taskStatusLogEntity)
        {
            try
            {
                _dbContext.TaskStatusLogs.Add(taskStatusLogEntity);
            }
            catch (ArgumentNullException argumentNullException)
            {
                argumentNullException.Source = nameof(taskStatusLogEntity);
                throw argumentNullException;
            }
        }


        public void Delete(TaskStatusLog taskStatusLog)
        {
            throw new NotImplementedException();
        }


        public List<TaskStatusLog> GetAll()
        {
            try
            {
                return _dbContext
                    .TaskStatusLogs
                    .ToList();
            }
            catch (NullReferenceException)
            {
                return new List<TaskStatusLog>();
            }
        }


        public TaskStatusLog GetById(int id)
        {
            TaskStatusLog? selectedTaskStatusLog = _dbContext.TaskStatusLogs.Where(tsl => tsl.Id == id).FirstOrDefault();

            if (selectedTaskStatusLog == null)
                return new TaskStatusLog();

            return selectedTaskStatusLog;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        public void Update(TaskStatusLog taskStatusLogEntity)
        {
            throw new NotImplementedException();
        }
    }
}