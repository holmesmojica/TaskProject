
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskProject.Domain.Enums;
using TaskProject.Infrastructure.Data.DataContext;


namespace TaskProject.Infrastructure.Data.Repository
{
    public class TaskStatusRepository : IRepository<Domain.Entities.TaskStatus>
    {
        private readonly DataContext.DataContextPostgreSQL _dbContext;


        internal TaskStatusRepository(IDataContext dbContext)
        {
            _dbContext = (DataContext.DataContextPostgreSQL)dbContext;
        }


        public void Create(Domain.Entities.TaskStatus taskStatusEntity)
        {
            try
            {
                _dbContext.TaskStatuses.Add(taskStatusEntity);
            }
            catch (ArgumentNullException argumentNullException)
            {
                argumentNullException.Source = nameof(taskStatusEntity);
                throw argumentNullException;
            }
        }


        public void Delete(Domain.Entities.TaskStatus taskStatus)
        {
            Domain.Entities.TaskStatus? selectedTaskStatus = GetById(taskStatus.Id);

            if (selectedTaskStatus == null)
                return;

            _dbContext.TaskStatuses.Remove(selectedTaskStatus);
        }


        public List<Domain.Entities.TaskStatus> GetAll()
        {
            try
            {
                return _dbContext
                    .TaskStatuses
                    .Where(ts => ts.Id != (int)TaskStatusEnum.DELETED)
                    .ToList();
            }
            catch(NullReferenceException)
            {
                return new List<Domain.Entities.TaskStatus>();
            }
        }


        public Domain.Entities.TaskStatus GetById(int id)
        {
            Domain.Entities.TaskStatus? selectedTaskStatus = _dbContext.TaskStatuses.Where(ts => ts.Id == id).FirstOrDefault();

            if (selectedTaskStatus == null)
                return new Domain.Entities.TaskStatus();

            return selectedTaskStatus;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        public void Update(Domain.Entities.TaskStatus taskStatusEntity)
        {
            Domain.Entities.TaskStatus? selectedTaskStatus = _dbContext.TaskStatuses.Where(t => t.Id == taskStatusEntity.Id).FirstOrDefault();

            if (selectedTaskStatus == null)
                return;

            selectedTaskStatus.Description = taskStatusEntity.Description;
            _dbContext.Entry(selectedTaskStatus).State = EntityState.Modified;
        }
    }
}