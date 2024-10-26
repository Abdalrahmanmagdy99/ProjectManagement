using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.DbContext;
using ProjectManagement.DTOs.Project;
using ProjectManagement.DTOs.Task;
using ProjectManagement.Entities;

namespace ProjectManagement.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectManagmentDbContext _context;

        public ProjectService(ProjectManagmentDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProjectAsync(AddProjectInputDto input)
        {
            if (input is null)
                return false;

            Project project = new Project
            {
                Budget = input.Budget,
                Description = input.Description,
                EndDate = input.EndDate,
                LastModefiedDate = DateTime.Now,
                Name = input.Name,
                OwnerId = input.OwnerId,
                StartDate = input.StartDate,
                Status = input.Status,
                Tasks = input.Tasks.IsNullOrEmpty() ? null : input.Tasks.Select(t=> new Entities.Task
                {
                    AssignedToId = t.AssignedTo,
                    Description = t.Description,
                    EndDate = t.EndDate,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    Status = t.Status,
                    Priority = t.Priority,
                    LastModefiedDate = DateTime.Now
                }).ToList()
            };
            await _context.Projects.AddAsync(project);
            return (await _context.SaveChangesAsync()) > 0;

        }
        public async Task<List<GetProjectOutputDto>> GetAllProjectsAsync()
        {
            return await _context.Projects.AsNoTracking()
                                 .Select(p => new GetProjectOutputDto
                                 {
                                     Budget = p.Budget,
                                     Description = p.Description,
                                     EndDate = p.EndDate,
                                     Status = p.Status,
                                     Owner = p.Owner.UserName,
                                     Name = p.Name,
                                     StartDate = p.StartDate,
                                     Tasks = p.Tasks.Select(t => new GetTaskOutputDto
                                     {
                                         Id = t.Id,
                                         StartDate = t.StartDate,
                                         Name = t.Name,
                                         Status = t.Status,
                                         AssignedTo = t.AssignedTo.UserName,
                                         Description = t.Description,
                                         Priority = t.Priority,
                                         EndDate = t.EndDate
                                     }).ToList()

                                 })
                                 .ToListAsync();
        }
        public async Task<GetProjectOutputDto> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects.AsNoTracking()
                                .Where(x=>x.Id == projectId)
                                .Select(p => new GetProjectOutputDto
                                {
                                    Budget = p.Budget,
                                    Description = p.Description,
                                    EndDate = p.EndDate,
                                    Status = p.Status,
                                    Owner = p.Owner.UserName,
                                    Name = p.Name,
                                    StartDate = p.StartDate,
                                    Tasks = p.Tasks.Select(t => new GetTaskOutputDto
                                    {
                                        Id= t.Id,
                                        StartDate = t.StartDate,
                                        Name = t.Name,
                                        Status = t.Status,
                                        AssignedTo = t.AssignedTo.UserName,
                                        Description = t.Description,
                                        Priority = t.Priority,
                                        EndDate = t.EndDate
                                    }).ToList()

                                })
                                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateProjectAsync(int projectId, UpdateProjectInputDto input)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(x=>x.Id == projectId);
            if (existingProject == null)
                return false;

            existingProject.Name = input.Name;
            existingProject.Description = input.Description;
            existingProject.StartDate = input.StartDate;
            existingProject.EndDate = input.EndDate;
            existingProject.LastModefiedDate = DateTime.Now;
            existingProject.Budget = input.Budget;
            existingProject.Status = input.Status;

            return (await _context.SaveChangesAsync()) > 0 ;
        }
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x=>x.Id == projectId);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
