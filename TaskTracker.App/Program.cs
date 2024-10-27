using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TaskTracker.App.Business;

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

        var taskStore = new TaskStore(){FilePath = Path.Combine(AppContext.BaseDirectory, "Data.json")};
        var taskTracker = new Business.TaskTracker(taskStore);

        if(args[0] == typeof(TaskCommand).GetField(TaskCommand.Add.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var description = args[1];
            var result = taskTracker.AddTask(description);
        }
        else if(args[0] == typeof(TaskCommand).GetField(TaskCommand.Update.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var description = args[2];
            var result = taskTracker.UpdateTaskDescription(id, description);
        }
        else if(args[0] == typeof(TaskCommand).GetField(TaskCommand.Delete.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var result = taskTracker.DeleteTask(id);
        }
        else if(args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkInProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var result = taskTracker.UpdateTaskStatus(id, Business.TaskStatus.InProgress);
        }
        else if(args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkDone.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var result = taskTracker.UpdateTaskStatus(id, Business.TaskStatus.Done);
        }
        else if(args[0] == typeof(TaskCommand).GetField(TaskCommand.List.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var tasks = new List<Business.Task>();
            if(string.IsNullOrEmpty(args[1])) 
            {
                tasks = taskTracker.ListAllTasks();
            }
            else
            {
                Business.TaskStatus taskStatus;
                if(args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Done.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.TaskStatus.Done;
                else if(args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.InProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.TaskStatus.InProgress;
                else if(args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Todo.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Business.TaskStatus.Todo;
                else
                    taskStatus = Business.TaskStatus.Todo;

                tasks = taskTracker.ListTasksByStatus(taskStatus);
            
            }
        }
        
    }
}
