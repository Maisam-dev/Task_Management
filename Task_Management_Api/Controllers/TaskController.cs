using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_Management_Api.DTOs;
using Task_Management_Api.Services.Interfaces;
using Task_Management_Api.Models;

namespace Task_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetAll()
        {
            if (User.IsInRole("Admin"))
            {
                return Ok(await _taskService.GetAll());
            }
            else if (User.IsInRole("User"))
            {
                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // read UserId from Token

                if (string.IsNullOrEmpty(claimUserId) || !int.TryParse(claimUserId, out int userId))
                {
                    return Unauthorized("UserId not found!");
                }
                return Ok(await _taskService.GetByUser(userId));
            }

            return Unauthorized("Role is not found");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("byId")]
        public async Task<ActionResult> GetById(int id)
        {
            var task = await _taskService.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post(TaskItem task)
        {
          var createdTask =  await _taskService.Post(task);
            if(createdTask == null)
            {
                return StatusCode(500,"internal Server error. please try it again");
            }
            return Ok(createdTask);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public async Task<ActionResult> Update(TaskItem task)
        {
            var updatedTask = await _taskService.UpdateTask(task);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var isUpdated = await _taskService.DeleteTask(id);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}