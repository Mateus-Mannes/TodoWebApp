namespace TodoApp.Domain
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TodoGroupId { get; set; }
        public TodoGroup TodoGroup { get; set; }
    }
}
