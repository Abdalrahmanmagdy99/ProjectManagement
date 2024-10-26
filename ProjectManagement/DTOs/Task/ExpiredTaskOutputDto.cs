using ProjectManagement.Enums;
using System.Text.Json.Serialization;

namespace ProjectManagement.DTOs.Task
{
    public class ExpiredTaskOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string ProjectName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PriorityEnum Priority { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatusEnum Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
