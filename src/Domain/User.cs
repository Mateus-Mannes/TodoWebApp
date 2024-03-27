namespace TodoApp.Domain;

public class User
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public string Slug { get => Name.Replace(' ', '\0').ToLower(); set { } }
    public required string PasswordHash { get; set; }
    public ICollection<TodoGroup> TodoGroups { get; set; } = new List<TodoGroup>();
}
