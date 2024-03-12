using CrudBlazorProject.Shared.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Tasks;
using Task = System.Threading.Tasks.Task;

await Task.Delay(TimeSpan.FromSeconds(2));

var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
var channel = GrpcChannel.ForAddress(new Uri("https://localhost:7018"), new GrpcChannelOptions(){HttpClient = httpClient});

var client = new TasksService.TasksServiceClient(channel);

Console.WriteLine(client.GetAllTasks(new Empty()));