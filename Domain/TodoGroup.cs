namespace TodoApp.Domain
{
    public class TodoGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int UserId { get; set; }

        public List<Todo> Todos { get; set; } 
        public User User { get; set; }

        public TodoGroup(string name, int userId)
        {
            Name = name;
            UserId = userId;
            Slug = name.ToLower().Replace(' ', '\0').ToLower();
        }

        public TodoGroup() { }
    }
}
