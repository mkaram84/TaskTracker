using TaskTracker.App.Business.Core;

namespace TaskTracker.App;

internal class Program
{
    static void Main(string[] args)
    {
        var taskStore = new TaskStore() { FilePath = Path.Combine(AppContext.BaseDirectory, "Data.json") };
        var taskTracker = new Business.Core.TaskTracker(taskStore);
        
        while (true)
        {
            Console.WriteLine("Enter command OR write Exit to end program: ");
            var command = Console.ReadLine()?.Split(" ");
            if (command?[0] == "Exit")
                break;

            var (result, tasks) = ArgumentProcessor.ProcessArguments(command, taskTracker);
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
        }
    }
}
