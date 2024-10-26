namespace ProjectManagement.DTOs.Comment
{
    public class CommentOutputDto
    {
        public int Id { get; set; }         
        public string Content { get; set; } 
        public string CreatedBy { get; set; } 
        public DateTime CreationDate { get; set; } 
        public int TaskId { get; set; }    

    }
}
