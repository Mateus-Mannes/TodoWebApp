using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TodoAppDbContext _context;
        private DbSet<T> table;
        public Repository(TodoAppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T obj)
        {
            table.Update(obj);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
