using ProjectManagement.Enums;

namespace ProjectManagement.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public PriorityEnum Priority { get; set; }  
        public TaskStatusEnum Status { get; set; }  
        public DateTime StartDate { get; set; }  
        public DateTime EndDate { get; set; }
        public DateTime LastModefiedDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public  List<Comment> Comments { get; set; }
    }
}
