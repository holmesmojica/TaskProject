
using TaskProject.Application.Dto;


namespace TaskProject.Application.Mapping
{
    internal class TaskMapper
    {
        public TaskDto Map(Domain.Entities.Task task)
        {
            TaskDto taskDto = new()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreationDate = task.CreationDate,
                TaskStatusId = task.TaskStatusLogs.First().TaskStatusId
            };

            return taskDto;
        }

        public Domain.Entities.Task Map(TaskDto taskDto)
        {
            Domain.Entities.Task task = new()
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                CreationDate = taskDto.CreationDate
            };

            if (taskDto.TaskStatusId > 0)
                task.TaskStatusLogs.Add(new Domain.Entities.TaskStatusLog() { Id = taskDto.TaskStatusId });

            return task;
        }

        public List<TaskDto> Map (List<Domain.Entities.Task> tasks)
        {
            List<TaskDto> taskDtos = new();

            foreach (Domain.Entities.Task task in tasks) 
            {
                taskDtos.Add(Map(task));
            }

            return taskDtos;
        }
    }
}