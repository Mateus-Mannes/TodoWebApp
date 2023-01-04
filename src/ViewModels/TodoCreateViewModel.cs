using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class TodoCreateViewModel
    {
        [Required]
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
        [Required]
        public int TodoGroupId { get; set; }
    }
}
