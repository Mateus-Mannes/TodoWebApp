namespace TodoApp.Domain;

public class Todo
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime CreatedAt { get; set; }
    public required int TodoGroupId { get; set; }
    public TodoGroup? TodoGroup { get; set; }
}

