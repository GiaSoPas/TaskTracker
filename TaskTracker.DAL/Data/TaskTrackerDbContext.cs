using Microsoft.EntityFrameworkCore;
using TaskTracker.DAL.Models;

namespace TaskTracker.DAL.Data;


public class TaskTrackerDbContext: DbContext
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options): base(options)
    {
        
    }
    
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
}