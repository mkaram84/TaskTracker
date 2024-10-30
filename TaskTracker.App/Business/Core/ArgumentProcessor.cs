using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TaskTracker.App.Business.Enum;

namespace TaskTracker.App.Business.Core;
public class ArgumentProcessor
{
    public static (bool, List<Domain.Task>?) ProcessArguments(string[] args, TaskTracker taskTracker)
    {
        if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Add.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var description = args[1];
            return (taskTracker.AddTask(description), null);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Update.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            var description = args[2];
            return (taskTracker.UpdateTaskDescription(id, description), null);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.Delete.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            return (taskTracker.DeleteTask(id), null);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkInProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            return (taskTracker.UpdateTaskStatus(id, Enum.TaskStatus.InProgress), null);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.MarkDone.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            var id = Convert.ToInt32(args[1]);
            return (taskTracker.UpdateTaskStatus(id, Enum.TaskStatus.Done), null);
        }
        else if (args[0] == typeof(TaskCommand).GetField(TaskCommand.List.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
        {
            List<Domain.Task> tasks;
            if (args.Length > 1)
            { 
                Enum.TaskStatus taskStatus;
                if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Done.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Enum.TaskStatus.Done;
                else if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.InProgress.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Enum.TaskStatus.InProgress;
                else if (args[1] == typeof(TaskSubCommandForList).GetField(TaskSubCommandForList.Todo.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name)
                    taskStatus = Enum.TaskStatus.Todo;
                else
                    taskStatus = Enum.TaskStatus.Todo;

                tasks = taskTracker.ListTasksByStatus(taskStatus);
            }
            tasks = taskTracker.ListAllTasks();
            return (true, tasks);
        }
        return (false, null);
    }
}
