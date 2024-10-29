using Task = TaskTracker.App.Business.Domain.Task;
using TaskStatus = TaskTracker.App.Business.Enum.TaskStatus;

namespace TaskTracker.App.Business.Core;

public class TaskTracker(TaskStore taskStore)
{
    public (bool, Task) AddTask(string description)
    {
        var task = new Task()
        {
            Description = description,
            Status = TaskStatus.Todo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        return (taskStore.AddTask(task), task);
    }

    public bool UpdateTaskDescription(int id, string description)
    {
        var task = taskStore.GetTaskById(id);
        if (task == null) return false;

        task.Description = description;
        task.UpdatedAt = DateTime.Now;
        return taskStore.UpdateTask(task);
    }

    public bool DeleteTask(int id)
    {
        return taskStore.DeleteTask(id);
    }

    public bool UpdateTaskStatus(int id, TaskStatus taskStatus)
    {
        var task = taskStore.GetTaskById(id);
        if (task == null) return false;

        task.Status = taskStatus;
        task.UpdatedAt = DateTime.Now;
        return taskStore.UpdateTask(task);
    }

    public List<Task> ListAllTasks()
    {
        return taskStore.GetAllTasks();
    }

    public List<Task> ListTasksByStatus(TaskStatus taskStatus)
    {
        var tasks = taskStore.GetAllTasks();
        return tasks.Where(task => task.Status == taskStatus).ToList();
    }
}
