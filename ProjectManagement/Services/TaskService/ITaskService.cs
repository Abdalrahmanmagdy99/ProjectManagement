using ProjectManagement.DTOs.Task;

namespace ProjectManagement.Services.TaskService
{
    public interface ITaskService
    {
        Task<bool> CreateTaskAsync(AddTaskInputDto input);
        Task<bool> DeleteTaskAsync(int taskId);
        Task<List<ExpiredTaskOutputDto>> GetExpiredTasksAsync();
        Task<bool> UpdateTaskAsync(int taskId, UpdateTaskInputDto input);
    }
}
