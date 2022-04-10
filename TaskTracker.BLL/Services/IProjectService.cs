using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.DAL.Models;

namespace TaskTracker.BLL.Services;

public interface IProjectService
{
    public Task<ProjectModel> GetProjectById(int id);
    public Task CreateProject(ProjectModel project);

    public Task<IEnumerable<ProjectModel>> GetProjects();

    public Task DeleteProjectById(int id);

    public Task UpdateProject(int id, ProjectModel project);

    public Task<IEnumerable<TaskModel>> GetTasksByProjectId(int id);

}