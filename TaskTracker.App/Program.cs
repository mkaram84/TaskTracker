using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using TaskTracker.App.Business;
using TaskTracker.App.Business.Core;
using TaskTracker.App.Business.Enum;

namespace TaskTracker.App;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }

        var taskStore = new TaskStore() { FilePath = Path.Combine(AppContext.BaseDirectory, "Data.json") };
        var taskTracker = new Business.Core.TaskTracker(taskStore);
        var result = false;
        result = ProcessArguments(args, taskTracker);

        if (result) Console.WriteLine("Successed!");

        Console.ReadLine();
    }

    public (bool, List<Task>) ProcessArguments(string[] args, Business.Core.TaskTracker taskTracker)
    {
        if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Add.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var description = args[1];
            return (taskTracker.AddTask(description), new List<Task> { });
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Update.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var description = args[2];
            result = taskTracker.UpdateTaskDescription(id, description);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Delete.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            result = taskTracker.DeleteTask(id);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkInProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            result = taskTracker.UpdateTaskStatus(id, Business.Enum.TaskStatus.InProgress);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkDone.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            result = taskTracker.UpdateTaskStatus(id, Business.Enum.TaskStatus.Done);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.List.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            List<Business.Domain.Task> tasks;
            if (string.IsNullOrEmpty(args[1]))
            {
                tasks = taskTracker.ListAllTasks();
            }
            else
            {
                Business.Enum.TaskStatus taskStatus;
                if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Done.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.Enum.TaskStatus.Done;
                else if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.InProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.Enum.TaskStatus.InProgress;
                else if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Todo.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.Enum.TaskStatus.Todo;
                else
                    taskStatus = Business.Enum.TaskStatus.Todo;

                tasks = taskTracker.ListTasksByStatus(taskStatus);
            }

            foreach (var task in tasks)
            {
                Console.WriteLine($"Task Info:");
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"CreatedAt: {task.CreatedAt}");
                Console.WriteLine($"UpdatedAt: {task.UpdatedAt}");
            }
            result = true;
        }

        return result;
    }
}
