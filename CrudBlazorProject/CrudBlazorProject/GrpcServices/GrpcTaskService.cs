using CrudBlazorProject.Shared.Services;
using Google.Protobuf.Collections;
using Grpc.Core;
using Tasks;
using Task = Tasks.Task;

namespace CrudBlazorProject.GrpcServices;

public class GrpcTaskService : TasksService.TasksServiceBase
{
    private readonly ITaskService _service;

    public GrpcTaskService(ITaskService service) => _service = service;

    public override async Task<GetTasksResponse> GetAllTasks(Empty request, ServerCallContext context)
    {
        var tasks = await _service.GetAllTasks();
        var result = new GetTasksResponse();
        result.Tasks.AddRange(tasks.Select(p=>(Task)p!));
        return result;
    }

    public override async Task<Task?> GetTaskById(TaskByIdRequest request, ServerCallContext context)
    {
        return await _service.GetTaskById(request.Id);
    }

    public override async Task<BoolResponse> DeleteTask(TaskByIdRequest request, ServerCallContext context)
    {
        return new BoolResponse()
        {
            Result = await _service.DeleteTaskById(request.Id)
        };
    }

    public override async Task<Task?> AddTask(Task request, ServerCallContext context)
    {
        return await _service.AddTask(request!);
    }

    public override async Task<Task?> UpdateTask(UpdateTaskRequest request, ServerCallContext context)
    {
        return await _service.UpdateTask(request.Id, request.Task!);
    }
}