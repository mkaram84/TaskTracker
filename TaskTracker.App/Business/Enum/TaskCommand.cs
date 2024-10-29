using System.ComponentModel.DataAnnotations;

namespace TaskTracker.App.Business.Enum;

public enum TaskCommand
{
    [Display(Name = "add")]
    Add = 1,
    [Display(Name = "update")]
    Update = 2,
    [Display(Name = "delete")]
    Delete = 3,
    [Display(Name = "mark-in-progress")]
    MarkInProgress = 4,
    [Display(Name = "mark-done")]
    MarkDone = 5,
    [Display(Name = "list")]
    List = 6
}