using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.DTOs.Comment;
using ProjectManagement.Services.CommentService;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase 
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
         [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDto input)
        {
            var result = await _commentService.AddCommentAsync(input);
            if (!result)
                return BadRequest("Failed to add comment.");

            return Ok("Comment added successfully.");
        }

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetCommentsByTaskId(int taskId)
        {
            var comments = await _commentService.GetCommentsByTaskIdAsync(taskId);

            return Ok(comments);
        }

    }
}
