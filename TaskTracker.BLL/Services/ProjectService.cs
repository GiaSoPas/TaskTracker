using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Exceptions;
using TaskTracker.DAL.Data;
using TaskTracker.DAL.Models;

namespace TaskTracker.BLL.Services;

public class ProjectService: IProjectService
{
    private readonly TaskTrackerDbContext _context;

    public ProjectService(TaskTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectModel>> GetProjects()
    {   
        var projects = await _context.Projects.ToListAsync();
        
        if (!projects.Any())
        {
            throw new NotFoundException($"No projects found");
        }

        return projects;
    }
    public async Task<ProjectModel> GetProjectById(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            throw new NotFoundException($"No projects with id = {id}");
        }

        return project;
    }
    public async Task CreateProject(ProjectModel project)
    {
        await _context.Projects.AddAsync(project);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProjectById(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project == null)
        {
            throw new NotFoundException($"No projects with id = {id}");
        }
        
        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateProject(int id, ProjectModel project)
    {
        if (id != project.Id)
        {
            throw new BadRequestException("id from the route is not equal to id from passed object");
        }

        var temp = await _context.Projects.FindAsync(id);
        if (temp == null)
        {
            throw new NotFoundException($"There is no project with id = {id}");
        }
        
        _context.Entry(project).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectId(int id)
    {
        var project = await _context.Projects
            .Include(project => project.Tasks)
            .Where(project => project.Id == id)
            .FirstOrDefaultAsync();
        
        if(project is null)
            throw new NotFoundException($"There is no project with {{id}} = {id}.");
        
        // var tasks = await _context.Tasks
        //     .Where(project => project.ProjectId == id)
        //     .ToListAsync();
        return project.Tasks;

    }

}