using CrudBlazorProject.Shared.Entity;

namespace CrudBlazorProject.Shared.Services;

public interface ITaskService
{
    public Task<TaskEntity[]> GetAllTasks();
    public Task<TaskEntity?> GetTaskById(ulong  id);
    public Task<bool> DeleteTaskById(ulong id);
    public Task<TaskEntity?> AddTask(TaskEntity task);
    public Task<TaskEntity?> UpdateTask(ulong id, TaskEntity task);
}