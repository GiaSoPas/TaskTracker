
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Exceptions;
using TaskTracker.DAL.Data;
using TaskTracker.DAL.Models;

namespace TaskTracker.BLL.Services;

public class TaskService : ITaskService
{
    private readonly TaskTrackerDbContext _context;

    public TaskService(TaskTrackerDbContext context)
    {
        _context = context;
    }

    public async Task CreateTask(TaskModel task)
    {
        var project = _context.Projects.FirstOrDefaultAsync(p => p.Id == task.ProjectId);
        if (project == null)
        {
            throw new NotFoundException($"No project with id = {task.ProjectId}");
        }
        
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskModel> GetTaskById(int id)
    {
        TaskModel task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            throw new NotFoundException($"No task with id = {id}");
        }
        return task;
    }

    public async Task<IEnumerable<TaskModel>> GetTasks()
    {
        var tasks = await _context.Tasks.ToListAsync();

        if (!tasks.Any())
        {
            throw new NotFoundException($"No tasks found");
        }

        return tasks;
    }

    public async Task DeleteTaskById(int id)
    {
        TaskModel task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            throw new NotFoundException($"There is no task with id = {id}");
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(int id, TaskModel task)
    {

        if (id != task.Id)
        {
            throw new BadRequestException("id from the route is not equal to id from passed object");
        }

        TaskModel temp = await _context.Tasks.FindAsync(id);
        if (temp == null)
        {
            throw new NotFoundException($"There is no task with id = {id}");
        }
        _context.Entry(task).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}