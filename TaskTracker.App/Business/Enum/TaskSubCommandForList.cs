using System.ComponentModel.DataAnnotations;

namespace TaskTracker.App.Business.Enum;

public enum TaskSubCommandForList
{
    [Display(Name = "todo")]
    Todo = 1,
    [Display(Name = "in-progress")]
    InProgress = 2,
    [Display(Name = "done")]
    Done = 3,
}

