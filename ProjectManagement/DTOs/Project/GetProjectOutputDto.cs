using ProjectManagement.DTOs.Task;
using ProjectManagement.Entities;
using ProjectManagement.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManagement.DTOs.Project
{
    public class GetProjectOutputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Budget { get; set; }
        public string Owner { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectStatusEnum Status { get; set; }
        public List<GetTaskOutputDto> Tasks { get; set; }
    }
}
