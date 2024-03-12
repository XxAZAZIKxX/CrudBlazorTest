using CrudBlazorProject.Shared.Entity;
using Tasks;

namespace CrudBlazorProject.Shared.Services;

public class TaskServiceClient : ITaskService
{
    private readonly TasksService.TasksServiceClient _client;

    public TaskServiceClient(TasksService.TasksServiceClient client) => _client = client;

    public async Task<TaskEntity[]> GetAllTasks()
    {
        var result = await _client.GetAllTasksAsync(new Empty());
        return result.Tasks.Where(p=>p is not null).Select(p=>(TaskEntity)p!).ToArray();
    }

    public async Task<TaskEntity?> GetTaskById(ulong id)
    {
        var result = await _client.GetTaskByIdAsync(new TaskByIdRequest() { Id = id });
        return result;
    }

    public async Task<bool> DeleteTaskById(ulong id)
    {
        var result = await _client.DeleteTaskAsync(new TaskByIdRequest() { Id = id });
        return result.Result;
    }

    public async Task<TaskEntity?> AddTask(TaskEntity task)
    {
        var result = await _client.AddTaskAsync(task);
        return result;
    }

    public async Task<TaskEntity?> UpdateTask(ulong id, TaskEntity task)
    {
        var result = await _client.UpdateTaskAsync(new UpdateTaskRequest()
        {
            Id = id,
            Task = task
        });

        return result;
    }
}