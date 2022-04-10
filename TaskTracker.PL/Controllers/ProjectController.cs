using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Services;
using TaskTracker.DAL.Data;
using TaskTracker.DAL.Models;

namespace TaskTracker.PL.Controllers
{
    [Route("api/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
        {
            return Ok(await _service.GetProjects());
        }
        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectModel))]
        public async Task<ActionResult> GetProject(int id)
        {
            return Ok(await _service.GetProjectById(id));
        }
        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectModel))]
        public async Task<ActionResult<ProjectModel>> CreateProject([FromBody] ProjectModel project)
        {
            await _service.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, project);
        }
        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProject(int id)
        {
            await _service.DeleteProjectById(id);
            return Ok();
        }
        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProject(int id, [FromBody] ProjectModel project)
        {
            await _service.UpdateProject(id, project);
            return Ok();
        }
        /// <summary>
        /// Get all task of the project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskModel>))]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksProject(int id)
        {
            return Ok(await _service.GetTasksByProjectId(id));
        } 
    }
}