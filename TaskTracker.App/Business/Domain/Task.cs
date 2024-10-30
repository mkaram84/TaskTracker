namespace TaskTracker.App.Business.Domain;
public class Task
{
    public int? Id { get; set; }
    public string? Description { get; set; }
    public Enum.TaskStatus Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
