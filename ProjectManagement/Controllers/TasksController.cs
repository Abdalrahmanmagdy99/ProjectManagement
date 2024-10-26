using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Constants;
using ProjectManagement.DTOs.Project;
using ProjectManagement.DTOs.Task;
using ProjectManagement.Managers;
using ProjectManagement.Services.ProjectService;
using ProjectManagement.Services.TaskService;

namespace ProjectManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly UserManager _userManager;

        public TaskController(ITaskService taskService,UserManager userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTaskInputDto input)
        {
            if (input == null)
                return BadRequest("Task data cannot be null.");

            var result = await _taskService.CreateTaskAsync(input);
            if (!result)
                return BadRequest("Failed to create the task or the project was not found.");

            return Ok("Task created successfully.");
        }
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskInputDto input)
        {
            if (input == null)
                return BadRequest("Task data cannot be null.");

            var currentUserId = _userManager.GetCurrentUserId();
            var currentUserRole = await _userManager.GetUserRoleAsync();
            if (input.AssignedToId != currentUserId && !currentUserRole.Equals(RolesNames.Manager))
                return Forbid("Action not allowed");

            var result = await _taskService.UpdateTaskAsync(id, input);
            if (!result)
                return NotFound("Task not found or update failed.");

            return Ok("Task updated successfully.");
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
                return BadRequest("Task not found or deletion failed.");

            return Ok("Task deleted successfully.");
        }

        [HttpGet(nameof(GetOverdueTasks))]
        public async Task<IActionResult> GetOverdueTasks()
        {
            var result = await _taskService.GetExpiredTasksAsync();


            return Ok(result);
        }
    }

}
