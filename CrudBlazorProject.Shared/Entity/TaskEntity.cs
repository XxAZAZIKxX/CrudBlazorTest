using Task = Tasks.Task;

namespace CrudBlazorProject.Shared.Entity;

public class TaskEntity
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string? Description  { get; set; }

    
    public static implicit operator TaskEntity?(Task? task)
    {
        if (task is null) return null;
        return new TaskEntity
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Desription
        };
    }

    public static implicit operator Task?(TaskEntity? task)
    {
        if(task is null) return null;
        return new Task()
        {
            Id = task.Id,
            Name = task.Name,
            Desription = task.Description
        };
    }
}