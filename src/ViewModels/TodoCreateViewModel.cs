using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public record TodoCreateViewModel
{
    [Required]
    public required string Description { get; init; }
    public DateTime? DeadLine { get; init; }
    [Required]
    public required int TodoGroupId { get; init; }
}

