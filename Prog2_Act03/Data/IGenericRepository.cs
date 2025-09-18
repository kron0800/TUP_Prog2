namespace Prog2_Act03.Data
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Save(T entity);
        Task<bool> Delete(int id);
    }
}
