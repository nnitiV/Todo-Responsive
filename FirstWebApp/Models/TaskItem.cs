using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }    = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public TaskItem(string title, string userId, int? categoryId )
        {
            Title = title;
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
