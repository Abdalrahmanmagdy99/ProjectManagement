using Microsoft.EntityFrameworkCore;
using ProjectManagement.DbContext;
using ProjectManagement.DTOs.Task;
using ProjectManagement.Enums;

namespace ProjectManagement.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ProjectManagmentDbContext _dbContext;

        public TaskService(ProjectManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateTaskAsync(AddTaskInputDto input)
        {
            if (input is null)
                return false;

            var project = await _dbContext.Projects.FirstOrDefaultAsync(x=>x.Id == input.ProjectId);
            if (project is null)
                return false;

            var task = new Entities.Task
            {
                Name = input.Name,
                Description = input.Description,
                AssignedToId = input.AssignedTo,
                Priority = input.Priority,
                Status = input.Status,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                ProjectId = input.ProjectId
            };

            await _dbContext.Tasks.AddAsync(task);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
        public async Task<bool> UpdateTaskAsync(int taskId, UpdateTaskInputDto input)
        {
            var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

            if (existingTask == null)
                return false;

            existingTask.Name = input.Name;
            existingTask.Description = input.Description;
            existingTask.Priority = input.Priority;
            existingTask.Status = input.Status;
            existingTask.StartDate = input.StartDate;
            existingTask.EndDate = input.EndDate;

            return (await _dbContext.SaveChangesAsync()) > 0;
        }
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
                return false;

            _dbContext.Tasks.Remove(task);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<List<ExpiredTaskOutputDto>> GetExpiredTasksAsync()
            => await _dbContext.Tasks.AsNoTracking()
            .Where(x => x.Status != TaskStatusEnum.Completed && x.EndDate < DateTime.Now)
            .Select(s => new ExpiredTaskOutputDto
            {
                EndDate = s.EndDate,
                AssignedTo = s.AssignedTo.UserName,
                Description = s.Description,
                Name = s.Name,
                Priority = s.Priority,
                ProjectName = s.Project.Name,
                Status = s.Status,
                StartDate = s.StartDate,
                Id = s.Id
            }).ToListAsync();
    }
}
