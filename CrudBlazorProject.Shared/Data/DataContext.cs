using CrudBlazorProject.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrudBlazorProject.Shared.Data;

public sealed class DataContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }


    public DataContext(DbContextOptions options) : base(options)
    {
    }
}