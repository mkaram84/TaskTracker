using System.Text.Json;
using Task = TaskTracker.App.Business.Domain.Task;

namespace TaskTracker.App.Business.Core;
public class TaskStore
{
    public required string FilePath { get; set; }

    public bool AddTask(Task task)
    {
        var tasks = GetAllTasks();
        task.Id = tasks.LastOrDefault()?.Id ?? 0 + 1;
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
        try
        {
            var json = File.ReadAllText(FilePath);
            //using JsonDocument doc = JsonDocument.Parse(json);
            return JsonSerializer.Deserialize<List<Task>>(json);
        }
        catch (JsonException)
        {
            return [];
        }
    }

    public Task? GetTaskById(int id)
    {
        var tasks = GetAllTasks();
        var index = tasks.FindIndex(task => task.Id == id);
        if (index != -1) return tasks[index];
        return null;
    }
}
