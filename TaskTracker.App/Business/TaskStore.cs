using System.Text.Json;

namespace TaskTracker.App.Business;
public class TaskStore
{
    public required string FilePath { get; set; }

    public bool AddTask(Task task)
    {
        var tasks = GetAllTasks();
        var nextId = tasks.LastOrDefault()?.Id + 1;
        task.Id = nextId;
        tasks.Add(task);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks));

        return true;
    }

    public bool UpdateTask(Task updatedTask)
    {
        var tasks = GetAllTasks();
        var index = tasks.FindIndex(task => task.Id == updatedTask.Id);
        if (index != -1)
        {
            tasks[index] = updatedTask;
            File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks));
        }

        return true;
    }

    public bool DeleteTask(int id)
    {
        var tasks = GetAllTasks();
        tasks.RemoveAll(task => task.Id == id);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks));

        return true;
    }

    public List<Task> GetAllTasks()
    {
        if (!File.Exists(FilePath))
        {
            File.CreateText(FilePath);
            return [];
        }
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Task>>(json) ?? [];
    }

    public Task? GetTaskById(int id)
    {
        var tasks = GetAllTasks();
        var index = tasks.FindIndex(task => task.Id == id);
        if (index != -1) return tasks[index];
        return null;
    }
}
