using ProjectManagement.DTOs.Project;

namespace ProjectManagement.Services.ProjectService
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(AddProjectInputDto input);
        Task<bool> DeleteProjectAsync(int projectId);
        Task<List<GetProjectOutputDto>> GetAllProjectsAsync();
        Task<GetProjectOutputDto> GetProjectByIdAsync(int projectId);
        Task<bool> UpdateProjectAsync(int projectId, UpdateProjectInputDto input);
    }
}
