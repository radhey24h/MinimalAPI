using Microsoft.EntityFrameworkCore;

namespace Employee.Dal.Repository.CommonRepository
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly EmployeeDbContext _dbContext;
        private DbSet<T> table;
        public CommonRepository(EmployeeDbContext context)
        {
            _dbContext = context;
            table = _dbContext.Set<T>();
        }
        public List<T> GetAll()
        {
            return table.ToList();
        }
        public T GetDetails(int id)
        {
            return table.Find(id);
        }
        public void Insert(T item)
        {
            table.Add(item);
        }
        public void Update(T item)
        {
            table.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(T item)
        {
            table.Remove(item);
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
