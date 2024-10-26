namespace ProjectManagement.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
