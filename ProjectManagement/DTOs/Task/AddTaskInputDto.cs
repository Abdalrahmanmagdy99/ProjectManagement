using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.Enums;
using ProjectManagement.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DTOs.Task
{
    public class AddTaskInputDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [RoleValidation(RolesNames.Employee)]
        public int AssignedTo { get; set; }
        [Range(1,4)]
        public PriorityEnum Priority { get; set; }
        [Range(1,3)]
        public TaskStatusEnum Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
