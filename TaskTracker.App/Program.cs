using TaskTracker.App.Business.Core;

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
        var (result, tasks) = ArgumentProcessor.ProcessArguments(args, taskTracker);
        if (tasks != null)
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task Info:");
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"CreatedAt: {task.CreatedAt}");
                Console.WriteLine($"UpdatedAt: {task.UpdatedAt}");
            }
        if (result) 
            Console.WriteLine("Successed!");

        Console.ReadLine();
    }
}
