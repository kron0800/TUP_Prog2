namespace Prog2_Act02.Services
{
    public interface IGenericApiService<T> where T: class
    {
        List<T> GetAll();
        T GetById(int id);
        int Save(T entity);
        bool Delete(int id);
    }
}
