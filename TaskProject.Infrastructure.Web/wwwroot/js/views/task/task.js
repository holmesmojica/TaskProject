
$(document).ready(() =>
{
    loadTasks();

    $(document).on('submit', '#taskCreationForm', validateCreationFormStatus);
    $(document).on('click', '#cancelUpdateTaskButton', finishEditTaskStatus);

    $('.nav .nav-item a').on('click', function (e)
    {
        e.preventDefault()
        $(e.target).tab('show')
    })
});


const loadTasks = async () =>
{
    let tasks = await requestAsync('task/GetAll', {});
    showTasks(tasks.data);
};


const cleanTasksLists = () =>
{
    $("#toBeDoneTaskListContainer").html("");
    $("#completedTaskListContainer").html("");
};


const showTasks = (tasks) =>
{
    let toBeDoneTasksHtml = "";
    let completedTasksHtml = "";

    cleanTasksLists();

    $.each(tasks, (index, task) =>
    {
        let isTaskCompleted = task.taskStatusId === 2;

        let taskHtml = getTaskHtml(task, isTaskCompleted);

        if (isTaskCompleted)
            completedTasksHtml += taskHtml;
        else
            toBeDoneTasksHtml += taskHtml;
    });
    
    $("#toBeDoneTaskListContainer").html(toBeDoneTasksHtml);
    $("#completedTaskListContainer").html(completedTasksHtml);
};


const getTaskHtml = (task, isTaskCompleted) =>
{
    let taskHeadHtml = getTaskHeaderHtml(task, isTaskCompleted);

    let taskHtml = `<div class="mb-2 border rounded overflow-hidden">
            ${taskHeadHtml}
            <div class="mt-1">
                <i>${task.description}</i>
            </div>
            <div class="d-flex justify-content-end mt-2 p-2 text-muted">
                <small>Created: ${task.formattedCreationDate}</small>
            </div>
        </div>`;

    return taskHtml;
};


const getTaskHeaderHtml = (task, isTaskCompleted) =>
{
    let headBackgroundClass = isTaskCompleted ? 'completed-task-head' : 'to-be-done-task-head';

    let checkBoxIput = isTaskCompleted ? ''
        : `<input class="form-check-input" type="checkbox" id="markCompletedTaskCheckBox_${task.id}"
                    onclick="proccessCompleteTaskButtonClick(${task.id});">`;

    let taskEditionButtons = isTaskCompleted ? ''
        : `<div>
            <button class="btn btn-outline-success btn-sm edit-task-btn" onclick="proccessEditTaskButtonClick(${task.id}, '${task.title}', '${task.description}');">
                <i class="bi bi-pencil"></i>
            </button>
            <button class="btn btn-outline-danger btn-sm delete-task-btn" onclick="proccessDeleteTaskButtonClick(${task.id});">
                <i class="bi bi-trash3"></i>
            </button>
          </div>`;

    let taskHeadHtml = `<div class="p-2 d-flex justify-content-center rounded-top ${headBackgroundClass}">
                ${checkBoxIput}
                <label class="form-check-label flex-fill" for="markCompletedTaskCheckBox_${task.id}">
                    <strong>${task.title}</strong>
                </label>
                ${taskEditionButtons}
            </div>`;

    return taskHeadHtml;
};


const validateCreationFormStatus = () =>
{
    event.preventDefault();

    let editStatus = $("#editButtonsContainer").hasClass('d-flex');

    if (editStatus)
        proccessUpdateTaskButtonClick();
    else
        proccessCreationFormSubmit();
};


const finishEditTaskStatus = () =>
{
    $("#taskCreationForm").get(0).reset();
    showCreateButton();
};


const proccessCreationFormSubmit = async () =>
{
    event.preventDefault();

    let result = await createTask();

    if (result.status === httpStatusCodes.CREATED)
    {
        $("#taskCreationForm").get(0).reset();
        loadTasks();
    }
};


const createTask = async () =>
{
    let params =
    {
        Title: $("#taskTitleInput").val(),
        Description: $("#taskDescriptionTextarea").val()
    };
    
    let result = await requestAsync('task/Create', params, requestMethods.POST);
    return result;
};


const proccessDeleteTaskButtonClick = async (taskId) =>
{
    let alertResult = await showAlert("Delete Task", "Are you sure?", notificationTypes.WARNING);

    if (!alertResult.value)
        return;

    let deleteResult = await deleteTask(taskId);

    if (deleteResult.status == httpStatusCodes.SUCCESS)
        loadTasks();
};


const deleteTask = async (taskId) =>
{
    let result = await requestAsync('task/Delete', { taskId }, requestMethods.DELETE);
    return result;
};


const proccessCompleteTaskButtonClick = async (taskId) =>
{
    let alertResult = await showAlert("Complete Task", "Are you sure?", notificationTypes.WARNING);

    if (!alertResult.value)
    {
        $(`#markCompletedTaskCheckBox_${taskId}`).prop("checked", false);
        return;
    }

    let completeResult = await completeTask(taskId);

    if (completeResult.status == httpStatusCodes.SUCCESS)
        loadTasks();
};


const completeTask = async (taskId) =>
{
    let result = await requestAsync('task/CompleteTask', { taskId }, requestMethods.PUT);
    return result;
};


const proccessEditTaskButtonClick = async (taskId, title, description) =>
{
    $("#taskTitleInput").val(title);
    $("#taskDescriptionTextarea").val(description);
    $("#updateTaskButton").val(taskId);
    showEditButtons();
};


const showEditButtons = () =>
{
    $("#createTaskButton").addClass('d-none');
    $("#editButtonsContainer").removeClass('d-none').addClass('d-flex');
};


const showCreateButton = () =>
{
    $("#createTaskButton").removeClass('d-none');
    $("#editButtonsContainer").removeClass('d-flex').addClass('d-none');
};


const proccessUpdateTaskButtonClick = async () =>
{
    let alertResult = await showAlert("Update Task", "Are you sure?", notificationTypes.WARNING);

    if (!alertResult.value)
        return;

    let updateResult = await updateTask();

    if (updateResult.status == httpStatusCodes.SUCCESS)
        loadTasks();

    finishEditTaskStatus();
};


const updateTask = async () =>
{
    let params =
    {
        Id: $("#updateTaskButton").val(),
        Title: $("#taskTitleInput").val(),
        Description: $("#taskDescriptionTextarea").val()
    };

    let result = await requestAsync('task/Update', params, requestMethods.PUT);
    return result;
};