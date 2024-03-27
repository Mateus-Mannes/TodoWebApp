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
        public IQueryable<T> GetQueryable()
        {
            return table;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task<T> InsertAsync(T obj)
        {
            var created = await table.AddAsync(obj);
            await _context.SaveChangesAsync();
            return created.Entity;
        }
        public async Task UpdateAsync(T obj)
        {
            table.Update(obj);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var existing = await table.FindAsync(id);
            if (existing is not null) table.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
