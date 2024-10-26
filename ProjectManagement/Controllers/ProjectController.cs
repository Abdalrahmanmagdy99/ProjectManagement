using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Constants;
using ProjectManagement.DTOs.Project;
using ProjectManagement.Services.ProjectService;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            if (projects.IsNullOrEmpty())
                return NotFound("No projects found.");

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project is null)
                return BadRequest($"Project not found.");

            return Ok(project);
        } 
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Identity.Bearer" , Roles = RolesNames.Manager)]
        public async Task<IActionResult> Add([FromBody] AddProjectInputDto input)
        {
            if (input is null)
                return BadRequest("Project data cannot be null.");

            var result = await _projectService.CreateProjectAsync(input);
            if (!result)
                return BadRequest("Failed to create the project.");

            return CreatedAtAction(nameof(Get), new { id = input.OwnerId }, "Project created successfully.");
        }
        [Authorize(AuthenticationSchemes = "Identity.Bearer", Roles = RolesNames.Manager)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectInputDto input)
        {
            if (input is null)
                return BadRequest("Project data cannot be null.");

            var result = await _projectService.UpdateProjectAsync(id, input);
            if (!result)
                return BadRequest($"Project with ID {id} not found or update failed.");

            return Ok("Project updated successfully.");
        }
        [Authorize(AuthenticationSchemes = "Identity.Bearer", Roles = RolesNames.Manager)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result)
                return BadRequest($"Project with ID {id} not found or deletion failed.");

            return Ok("Project deleted successfully.");
        }
    }
}
