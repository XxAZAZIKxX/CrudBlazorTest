using CrudBlazorProject.Shared.Services;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tasks;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



builder.Services
    .AddScoped(_=>new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())))
    .AddScoped(provider => GrpcChannel.ForAddress(new Uri("https://localhost:7018"), new GrpcChannelOptions()
    {
        HttpClient = provider.GetService<HttpClient>()
    }));
builder.Services
    .AddScoped(provider => new TasksService.TasksServiceClient(provider.GetService<GrpcChannel>()))
    .AddScoped<ITaskService, TaskServiceClient>();


await builder.Build().RunAsync();