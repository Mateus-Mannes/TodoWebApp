namespace TodoApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);
        IQueryable<T> GetQueryable();
    }
}
