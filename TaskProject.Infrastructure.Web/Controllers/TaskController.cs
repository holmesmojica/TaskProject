
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskProject.Application.DBServices;
using TaskProject.Application.Dto;


namespace TaskProject.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly DBTaskService _taskService;
        private readonly DBTaskStatusService _taskStatusService;


        public TaskController()
        {
            _taskService = new DBTaskService();
            _taskStatusService = new DBTaskStatusService();
        }


        // GET: TaskController
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            List<TaskDto> tasks = _taskService.GetAll();
            return BuildStatusResponse(HttpStatusCode.Found, tasks);
        }


        [HttpPost]
        public ActionResult Create(TaskDto taskDto)
        {
            TaskDto createdTask = _taskService.CreateTask(taskDto);
            return BuildStatusResponse(HttpStatusCode.Created, createdTask);
        }


        [HttpPut]
        public ActionResult CompleteTask(int taskId)
        {
            _taskService.MarkTaskAsComplete(taskId);
            return BuildStatusResponse(HttpStatusCode.OK);
        }

        
        [HttpPut]
        public ActionResult Update(TaskDto taskDto)
        {
            _taskService.UpdateTask(taskDto);
            return BuildStatusResponse(HttpStatusCode.OK);
        }

        
        [HttpDelete]
        public ActionResult Delete(int taskId)
        {
            _taskService.DeleteTask(taskId);
            return BuildStatusResponse(HttpStatusCode.OK);
        }


        private ActionResult BuildStatusResponse(HttpStatusCode statusCode, object? data = null)
        {
            object response = new
            {
                Status = statusCode,
                Data = data
            };

            return Json(response);
        }
    }
}