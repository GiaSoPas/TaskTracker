using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using TaskTracker.BLL.Services;
using TaskTracker.DAL.Data;
using TaskTracker.DAL.Models;

namespace TaskTracker.PL.Controllers
{
    [Route("api/tasks")]
    [Produces("application/json")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get task by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            TaskModel task = await _service.GetTaskById(id);
            return Ok(task);
        }
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return Ok(await _service.GetTasks());
        }
        
        /// <summary>
        /// Create new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel task)
        {
            await _service.CreateTask(task);

            return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task);

        }
        /// <summary>
        /// Delete Task by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTask(int id)
        {
            await _service.DeleteTaskById(id);

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTask(int id, [FromBody]TaskModel task)
        {
            await _service.UpdateTask(id, task);

            return Ok();
        }
    }
}