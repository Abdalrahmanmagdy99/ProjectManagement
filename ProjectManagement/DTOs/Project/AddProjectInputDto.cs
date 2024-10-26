using ProjectManagement.Constants;
using ProjectManagement.DTOs.Task;
using ProjectManagement.Entities;
using ProjectManagement.Enums;
using ProjectManagement.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DTOs.Project
{
    public class AddProjectInputDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        [RoleValidation(RolesNames.Manager)]
        public int OwnerId { get; set; }
        [Range(1,3)]
        [Required]
        public ProjectStatusEnum Status { get; set; }
        public List<AddTaskInputDto> Tasks { get; set; }
    }
}
