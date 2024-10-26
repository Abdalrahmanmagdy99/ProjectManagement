using Microsoft.EntityFrameworkCore;
using ProjectManagement.DbContext;
using ProjectManagement.DTOs.Comment;
using ProjectManagement.Entities;
using ProjectManagement.Managers;
using System.Security.Claims;

namespace ProjectManagement.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ProjectManagmentDbContext _dbContext;
        private readonly UserManager _userManager;

        public CommentService(ProjectManagmentDbContext dbContextm,UserManager userManager)
        {
            _dbContext = dbContextm;
            _userManager = userManager;
        }
        public async Task<bool> AddCommentAsync(AddCommentDto input)
        {
            if (input == null || string.IsNullOrEmpty(input.Content))
               return false;

            var userId = _userManager.GetCurrentUserId();
            if (userId < 0)
                return false;

            var taskExists = await _dbContext.Tasks.AnyAsync(t => t.Id == input.TaskId);
            if (!taskExists)
                return false;

            var comment = new Comment
            {
                Content = input.Content,
                TaskId = input.TaskId,
                CreatedById = userId,
                CreationDate = DateTime.Now
            };

            await _dbContext.Comments.AddAsync(comment);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
        public async Task<List<CommentOutputDto>> GetCommentsByTaskIdAsync(int taskId)
        {
          return await _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.TaskId == taskId)
                .Include(c => c.CreatedBy)
                .Select(c => new CommentOutputDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedBy = c.CreatedBy.UserName,
                    CreationDate = c.CreationDate,
                    TaskId = c.TaskId
                })
                .ToListAsync();
        }
    }
}
