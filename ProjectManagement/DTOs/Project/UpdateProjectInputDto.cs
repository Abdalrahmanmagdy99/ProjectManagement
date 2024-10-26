using ProjectManagement.DTOs.Task;
using ProjectManagement.Entities;
using ProjectManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DTOs.Project
{
    public class UpdateProjectInputDto
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
        [Range(1,3)]
        public ProjectStatusEnum Status { get; set; }
    }
}
