namespace TodoApp.ViewModels
{
    public class TodoCreateViewModel
    {
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
        public int TodoGroupId { get; set; }
    }
}
