namespace TodoApp.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string PasswordHash { get; set; }

        public List<TodoGroup> TodoGroups { get; set; } = new List<TodoGroup>();
    }
}
