using ProjectManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DTOs.Task
{
    public class UpdateTaskInputDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int AssignedToId { get; set; }
        [Range(1, 4)]
        public PriorityEnum Priority { get; set; }
        [Range(1, 3)]
        public TaskStatusEnum Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
