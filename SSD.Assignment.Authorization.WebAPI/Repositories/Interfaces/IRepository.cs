namespace SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

public interface IRepository<T>
{
    T Add(T entity);
    IEnumerable<T> GetAll();
    T Get(int id);
    T Update(T entity);
    T Delete(int id);
}