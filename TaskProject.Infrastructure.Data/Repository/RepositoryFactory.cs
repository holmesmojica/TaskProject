
using TaskProject.Infrastructure.Data.DataContext;


namespace TaskProject.Infrastructure.Data.Repository
{
    public class RepositoryFactory
    {
        private readonly IDataContext _dataContext;


        //Podría inyectarse aquí el datacontext para permitir uso de diferentes motores de DB, en caso en que se necesitara.
        //Para este caso decidí entregarle al Factory el control al decidir que motor de DB usar.
        public RepositoryFactory()
        {
            _dataContext = new DataContextPostgreSQL();
        }


        public IRepository<Domain.Entities.Task> GetTaskRepository()
        {
            IRepository<Domain.Entities.Task> taskRepository = new TaskRepository(_dataContext);
            return taskRepository;
        }


        public IRepository<Domain.Entities.TaskStatus> GetTaskStatusRepository()
        {
            IRepository<Domain.Entities.TaskStatus> taskStatusRepository = new TaskStatusRepository(_dataContext);
            return taskStatusRepository;
        }


        public IRepository<Domain.Entities.TaskStatusLog> GetTaskStatusLogRepository()
        {
            IRepository<Domain.Entities.TaskStatusLog> taskStatusLogRepository = new TaskStatusLogRepository(_dataContext);
            return taskStatusLogRepository;
        }
    }
}