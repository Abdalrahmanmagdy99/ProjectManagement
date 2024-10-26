using ProjectManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DTOs.Comment
{
    public class AddCommentDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int TaskId { get; set; }
    }
}
