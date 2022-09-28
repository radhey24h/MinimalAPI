namespace Employee.Dal.Repository.CommonRepository
{
    public interface ICommonRepository<T>
    {
        List<T> GetAll();
        T GetDetails(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        int SaveChanges();
    }
}
