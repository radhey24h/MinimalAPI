namespace Employee.Dal.Repository.AsyncCommonRepository
{
    public interface IAsyncCommonRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetDetails(int id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
