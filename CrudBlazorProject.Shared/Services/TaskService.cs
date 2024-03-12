using CrudBlazorProject.Shared.Data;
using CrudBlazorProject.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrudBlazorProject.Shared.Services;

public class TaskService : ITaskService
{
    private readonly DataContext _dataContext;

    public TaskService(DataContext dataContext) => _dataContext = dataContext;

    public async Task<TaskEntity[]> GetAllTasks()
    {
        return await _dataContext.Tasks.ToArrayAsync();
    }

    public async Task<TaskEntity?> GetTaskById(ulong id)
    {
        return await _dataContext.Tasks.FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task<bool> DeleteTaskById(ulong id)
    {
        var task = await _dataContext.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        if (task is null) return false;
        
        _dataContext.Remove(task);
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<TaskEntity?> AddTask(TaskEntity task)
    {
        var result = await _dataContext.Tasks.AddAsync(task);
        await _dataContext.SaveChangesAsync();
        return result.Entity;

    }

    public async Task<TaskEntity?> UpdateTask(ulong id, TaskEntity task)
    {
        var find = await _dataContext.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        if (find is null) return null;
        find.Name = task.Name;
        find.Description = task.Description;
        await _dataContext.SaveChangesAsync();
        return find;
    }
}