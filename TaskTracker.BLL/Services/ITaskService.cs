using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.DAL.Models;

namespace TaskTracker.BLL.Services;

public interface ITaskService
{
    public Task CreateTask(TaskModel task);

    public Task<TaskModel> GetTaskById(int id);

    public Task DeleteTaskById(int id);

    public Task UpdateTask(int id, TaskModel task);

    public Task<IEnumerable<TaskModel>> GetTasks();
}