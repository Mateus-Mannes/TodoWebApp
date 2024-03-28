using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public record TodoUpdateViewModel
{
    [Required]
    public required string Description { get; set; }
    public DateTime? DeadLine { get; set; }
}
