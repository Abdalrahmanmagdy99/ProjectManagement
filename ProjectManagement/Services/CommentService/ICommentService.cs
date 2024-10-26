using ProjectManagement.DTOs.Comment;

namespace ProjectManagement.Services.CommentService
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(AddCommentDto input);
        Task<List<CommentOutputDto>> GetCommentsByTaskIdAsync(int taskId);
    }
}
