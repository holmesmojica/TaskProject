
using TaskProject.Infrastructure.Data.Repository;
using TaskProject.Domain.Entities;
using TaskProject.Domain.Enums;
using TaskProject.Application.Dto;
using TaskProject.Application.Mapping;


namespace TaskProject.Application.DBServices
{
    public class DBTaskService
    {
        private readonly IRepository<Domain.Entities.Task> _taskRepository;
        private readonly IRepository<TaskStatusLog> _taskStatusLogRepository;

        private readonly TaskMapper _taskMapper;

        public DBTaskService()
        {
            RepositoryFactory repositoryFactory = new();

            _taskRepository = repositoryFactory.GetTaskRepository();
            _taskStatusLogRepository = repositoryFactory.GetTaskStatusLogRepository();

            _taskMapper = new();
        }


        public TaskDto CreateTask(TaskDto taskDto)
        {
            if (taskDto == null)
                throw new ArgumentNullException(nameof(taskDto));

            taskDto.CreationDate = DateTime.Now;
            Domain.Entities.Task task = _taskMapper.Map(taskDto);
            _taskRepository.Create(task);
            _taskRepository.SaveChanges();

            CreateTaskStatusLog(task, (int)TaskStatusEnum.PENDING);
            return _taskMapper.Map(task);
        }


        private void CreateTaskStatusLog (Domain.Entities.Task task, int taskStatusId)
        {
            TaskStatusLog? taskStatusLog = new()
            {
                Task = task,
                TaskId = task.Id,
                StatusChangeDate = DateTime.Now,
                TaskStatusId = taskStatusId
            };

            _taskStatusLogRepository.Create(taskStatusLog);
            _taskStatusLogRepository.SaveChanges();
        }


        public void MarkTaskAsComplete(int taskId)
        {
            Domain.Entities.Task task = _taskRepository.GetById(taskId);

            if (task == null)
                throw new NullReferenceException(nameof(task));

            CreateTaskStatusLog(task, (int)TaskStatusEnum.COMPLETED);
        }


        public void UpdateTask(TaskDto taskDto)
        {
            if (taskDto == null)
                throw new ArgumentNullException(nameof(taskDto));

            Domain.Entities.Task task = _taskRepository.GetById(taskDto.Id);

            if (task == null)
                throw new NullReferenceException(nameof(task));

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;

            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
        }


        //Esta función lo que hace realmente es crear un nuevo estado para la tarea en lugar de borrarla
        //El nuevo estado es DELETED, lo que indica que esa tarea no se pódrá ver ni modificar más
        //El propósito de realizar el proceso así es mantener el historial de las tareas aún cuando han sido eliminadas y también cuidar la integridad de los datos en la DB
        //Sin embargo en las clases repositorio están los métodos delete que se crearon como demostración de la funcionalidad
        public void DeleteTask(int taskId)
        {
            if (taskId == 0)
                throw new ArgumentNullException(nameof(taskId));

            Domain.Entities.Task task = _taskRepository.GetById(taskId);
            CreateTaskStatusLog(task, (int)TaskStatusEnum.DELETED);
        }

        public List<TaskDto> GetAll()
        {
            List<Domain.Entities.Task>? tasks = _taskRepository.GetAll();
            return _taskMapper.Map(tasks);
        }

        public TaskDto GetById(int taskId)
        {
            Domain.Entities.Task? task = _taskRepository.GetById(taskId);
            return _taskMapper.Map(task);
        }
    }
}