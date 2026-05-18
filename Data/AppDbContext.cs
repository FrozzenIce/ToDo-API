namespace ToDo.Data;

using Microsoft.EntityFrameworkCore; 
using ToDo.Models;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public DbSet<Tasks> Tasks { get; set; }
}