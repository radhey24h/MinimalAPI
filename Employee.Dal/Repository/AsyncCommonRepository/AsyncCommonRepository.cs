using Microsoft.EntityFrameworkCore;

namespace Employee.Dal.Repository.AsyncCommonRepository
{
    public class AsyncCommonRepository<T> : IAsyncCommonRepository<T> where T : class
    {
        private readonly EmployeeDbContext _dbContext;
        public AsyncCommonRepository(EmployeeDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetDetails(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Delete(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
