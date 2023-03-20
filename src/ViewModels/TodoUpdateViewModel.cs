using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class TodoUpdateViewModel
    {
        [Required]
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
