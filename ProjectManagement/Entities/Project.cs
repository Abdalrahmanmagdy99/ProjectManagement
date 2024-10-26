using ProjectManagement.Enums;

namespace ProjectManagement.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastModefiedDate { get; set; }
        public decimal Budget { get; set; }
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public ProjectStatusEnum Status{ get; set; }
        public  List<Task> Tasks { get; set; }
    }
}
