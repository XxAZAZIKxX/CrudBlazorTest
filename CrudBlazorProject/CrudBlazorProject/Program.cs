using CrudBlazorProject.Components;
using CrudBlazorProject.GrpcServices;
using CrudBlazorProject.Shared.Data;
using CrudBlazorProject.Shared.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<DataContext>(optionsBuilder =>
{
    var configurationSection = builder.Configuration.GetSection("DbConnection");
    var connectionString = new MySqlConnectionStringBuilder
    {
        Database = configurationSection.GetSection("Database").Value,
        UserID = configurationSection.GetSection("UserID").Value,
        Password = configurationSection.GetSection("Password").Value,
        Server = configurationSection.GetSection("Server").Value,
        Port = uint.Parse(configurationSection.GetSection("Port").Value!)
    }.ConnectionString;
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    optionsBuilder.UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddGrpc();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseGrpcWeb();

app.MapGrpcService<GrpcTaskService>().EnableGrpcWeb();


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CrudBlazorProject.Client._Imports).Assembly);

app.Run();
