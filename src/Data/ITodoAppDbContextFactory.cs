namespace TodoApp.Data
{
    public interface ITodoAppDbContextFactory
    {
        TodoAppDbContext CreateContext();
    }

}
