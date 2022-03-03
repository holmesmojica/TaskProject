
using TaskProject.Infrastructure.Data.Repository;


namespace TaskProject.Application.DBServices
{
    public class DBTaskStatusService
    {
        private readonly IRepository<Domain.Entities.TaskStatus> _taskStatusRepository;


        public DBTaskStatusService()
        {
            RepositoryFactory repositoryFactory = new();
            _taskStatusRepository = repositoryFactory.GetTaskStatusRepository();
        }


        public List<Domain.Entities.TaskStatus> GetAll()
        {
            return _taskStatusRepository.GetAll();
        }
    }
}